
using UnityEngine;
using Defective.JSON;
using System.Collections.Generic;

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
                Debug.Log(resID);
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
}
