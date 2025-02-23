using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;

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
    /// 背包
    /// </summary>
    public Knapsack knapsack{ get; private set; }

    /// <summary>
    /// 物品数据列表
    /// </summary>
    public List<ItemData> itemDataList= new List<ItemData>();

    private void Init()
    {
        _instance = this;
        knapsack = Knapsack.Instance;
        ParseItemJson();
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 解析Json配置表获取物品数据
    /// </summary>
    private void ParseItemJson()
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
                        itemDataList.Add(new ConsumableData(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp));
                        break;
                    case ItemData.ItemType.Equipment:
                        int strength = temp["strength"].intValue;
                        int intellect = temp["intellect"].intValue;
                        int agility = temp["agility"].intValue;
                        int stamina = temp["stamina"].intValue;
                        EquipmentData.EquipmentType equipType = (EquipmentData.EquipmentType)System.Enum.Parse(typeof(EquipmentData.EquipmentType), temp["equipType"].stringValue);
                        itemDataList.Add(new EquipmentData(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite,
                            strength, intellect, agility, stamina, equipType));
                        break;
                    case ItemData.ItemType.Weapon:
                        int damage = temp["damage"].intValue;
                        WeaponData.WeaponType weaponType = (WeaponData.WeaponType)System.Enum.Parse(typeof(WeaponData.WeaponType), temp["weaponType"].stringValue);
                        itemDataList.Add(new WeaponData(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, weaponType));
                        break;
                    case ItemData.ItemType.Material:
                        itemDataList.Add(new MaterialData());
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

    /// <summary>
    /// 通过ID获取物品数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ItemData GetItemData(int itemId)
    {
        foreach (ItemData itemData in itemDataList)
        {
            if (itemData.ID == itemId)return itemData;
        }
        Debug.LogError($"未能找到ID为{itemId}的物品");
        return null;
    }
}
