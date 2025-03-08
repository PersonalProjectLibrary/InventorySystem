
using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 物品管理类
/// </summary>
public class InventoryManager : MonoBehaviour
{
    #region 单例模式
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    #endregion
    
    /// <summary>
    /// 物品数据列表[配置文件解析得到]
    /// </summary>
    public Dictionary<int, ItemData> itemDataDic = new Dictionary<int, ItemData>();
    //背包
    public Knapsack knapsack{ get; private set; }
    public Chest chest{ get; private set; }
    //物品信息提示框
    public InfoTips tips{ get; private set; }
    private bool isTipsShow = false;
    private RectTransform canvasRect;
    //当前选中的物品
    public Item PickedItem{ get; private set; }
    private bool isPickedItem = false;
    public bool IsPickedItem
    {
        get { return isPickedItem; }
    }

    private void InitInventoryMgr()
    {
        _instance = this;
        tips = InfoTips.Instance;
        knapsack = Knapsack.Instance;
        chest = Chest.Instance;
        canvasRect = GameObject.Find("Canvas").GetComponent<Canvas>().transform as RectTransform;
        PickedItem = GameObject.Find("PickedItem").GetComponent<Item>();
        PickedItem.Hide();
        InitItemDataList();
    }

    private void Start()
    {
        InitInventoryMgr();
    }

    public void Update()
    {
        if (isTipsShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out position);
            tips.SetLocalPosition(position + Constant.TipsPosOffset);
        }
        if (isPickedItem)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out position);
            PickedItem.SetLocalPosition(position);
        }

        //物品丢弃处理
        if (isPickedItem && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(-1))
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
    }

    //射线检测判断鼠标是否在UI上
    bool IsPointerOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    
    private void InitItemDataList()
    {
        try
        {
            TextAsset itemText = Resources.Load<TextAsset>("Items");
            if (itemText == null)
            {
                Debug.LogError("未能找到Items资源文件");
                return;
            }
            string itemJson = itemText.text;
            JSONObject j = new JSONObject(itemJson);
            foreach (JSONObject temp in j.list)
            {
                int id = temp["id"].intValue;
                string name = temp["name"].stringValue;
                ItemData.ItemType type = (ItemData.ItemType)System.Enum.Parse(typeof(ItemData.ItemType), temp["type"].stringValue);
                ItemData.ItemQuality quality = (ItemData.ItemQuality)System.Enum.Parse(typeof(ItemData.ItemQuality), temp["quality"].stringValue);
                string description = temp["description"].stringValue;
                int capacity = temp["capacity"].intValue;
                int buyPrice = temp["buyPrice"].intValue;
                int sellPrice = temp["sellPrice"].intValue;
                string sprite = temp["sprite"].stringValue;
                switch (type)
                {
                    case ItemData.ItemType.Consumable:
                        int hp = temp["hp"].intValue;
                        int mp = temp["mp"].intValue;
                        itemDataDic.Add(id, new ConsumableData(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp));
                        break;
                    case ItemData.ItemType.Equipment:
                        int strength = temp["strength"].intValue;
                        int intellect = temp["intellect"].intValue;
                        int agility = temp["agility"].intValue;
                        int stamina = temp["stamina"].intValue;
                        EquipmentData.EquipmentType equipType = (EquipmentData.EquipmentType)System.Enum.Parse(typeof(EquipmentData.EquipmentType), temp["equipType"].stringValue);
                        itemDataDic.Add(id, new EquipmentData(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite,
                        strength, intellect, agility, stamina, equipType));
                        break;
                    case ItemData.ItemType.Weapon:
                        int damage = temp["damage"].intValue;
                        WeaponData.WeaponType weaponType = (WeaponData.WeaponType)System.Enum.Parse(typeof(WeaponData.WeaponType), temp["weaponType"].stringValue);
                        itemDataDic.Add(id, new WeaponData(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, weaponType));
                        break;
                    case ItemData.ItemType.Material:
                        itemDataDic.Add(id, new MaterialData(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite));
                        break;
                }
                //Debug.Log(name);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"解析发生错误：{e.Message}");
        }
    }
    public ItemData GetItemDataById(int itemId)
    {
        if(!itemDataDic.ContainsKey(itemId))
        {
            Debug.LogError($"未能找到ID为{itemId}的物品");
            return null;
        }
        return itemDataDic[itemId];
    }

    public void ShowItemTips(string info)
    {
        isTipsShow = true;
        tips.ShowTips(info);
    }
    public void HideItemTips()
    {
        isTipsShow = false;
        tips.HideTips();
    }

    public void PickUpItem(Item item, int itemCount)
    {
        PickedItem.InitItem(item.selfData);
        PickedItem.UpdateItemCount(itemCount);
        isPickedItem = true;
        PickedItem.Show();

        isTipsShow = false;
        tips.HideTips();
    }
    public void UpdatePickedItem(int count)
    {
        if (count == 0)
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
        else PickedItem.UpdateItemCount(count);
    }
}
