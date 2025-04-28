
using UnityEngine;
using Defective.JSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class ForgePanel : InventoryPanel
{
    #region 单例模式
    private static ForgePanel _instance;
    public static ForgePanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ForgePanel").GetComponent<ForgePanel>();
            }
            return _instance;
        }
    }
    #endregion

    private List<FormulaData> formulaDatas = new List<FormulaData>();
    private Button forgeBtn;
    
    public void Awake()
    {
        InitFormulaDatas();
    }
    private void InitFormulaDatas()
    {
        try
        {
            TextAsset formulaText = Resources.Load<TextAsset>("Formulas");
            if (formulaText == null)
            {
                Debug.LogError("未能找到Formulas资源文件");
                return;
            }
            string formulaJson = formulaText.text;
            JSONObject j = new JSONObject(formulaJson);
            foreach (JSONObject temp in j.list)
            {
                int item1ID = temp["Item1ID"].intValue;
                int item1Amount = temp["Item1Amount"].intValue;
                int item2ID = temp["Item2ID"].intValue;
                int item2Amount = temp["Item2Amount"].intValue;
                int resID = temp["ResID"].intValue;
                formulaDatas.Add(new FormulaData(item1ID, item1Amount, item2ID, item2Amount, resID));
                //Debug.Log(resID);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"解析发生错误：{e.Message}");
        }
    }

    protected override void Init()
    {
        slotCount = Constant.ForgeSlotCount;
        forgeBtn = transform.Find("ForgeBtn").GetComponent<Button>();
        forgeBtn.onClick.AddListener(ForgeItem);
        base.Init();
    }
    protected override void InstanticeSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotGo.name = $"Slot{i+1}";
            slotList.Add(slotGo.GetComponent<ForgeSlot>());
        }
    }
    private void ForgeItem()
    {
        //Debug.Log("Forgebtn点击");
        Dictionary<int,int> hadMaterials = new Dictionary<int,int>();//当前拥有的[材料，个数]
        foreach (var slot in slotList)
        {
            if (!slot.IsEmpty())
            {
                if(!hadMaterials.ContainsKey(slot.item.selfData.ID))
                {
                    hadMaterials.Add(slot.item.selfData.ID,slot.item.ItemAmount);
                }
                else
                {
                    hadMaterials[slot.item.selfData.ID] += slot.item.ItemAmount;
                }
            }
        }
        //Debug.Log($"当前拥有：{hadMaterials.Count}种物品");
        if(hadMaterials.Count == 0)return;
        FormulaData matchedFormula = null; 
        foreach (var formulaData in formulaDatas)
        {
            if(!formulaData.IsMatch(hadMaterials))continue;
            matchedFormula = formulaData;
            break;
        }
        //Debug.Log($"是否找到匹配配方{matchedFormula!=null}");
        if(matchedFormula == null)return;
        inventoryMgr.knapsackPanel.ObtainItem(matchedFormula.ResultItemId);//背包例添加锻造出来的物品
        foreach (var slot in slotList)//锻造面板里删除使用过的物品
        {
            if (!slot.IsEmpty())
            {
                if (slot.item.selfData.ID == matchedFormula.Item1Id)
                {
                    int amount = slot.item.ItemAmount-matchedFormula.Item1Count;
                    slot.UpdateItemCount(amount);
                }
                else if (slot.item.selfData.ID == matchedFormula.Item2Id)
                {
                    int amount = slot.item.ItemAmount-matchedFormula.Item2Count;
                    slot.UpdateItemCount(amount);
                }
            }
        }
    }
}
