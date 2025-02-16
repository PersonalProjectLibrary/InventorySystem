using System.Collections;
using System.Collections.Generic;
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
    
    public List<Item> itemList= new List<Item>();//物品列表

    private void Start()
    {
        ParseItemJson();
    }

    /// <summary>
    /// 解析物品json
    /// </summary>
    void ParseItemJson()
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
            Debug.Log(itemJson);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"解析发生错误：{e.Message}");
        }
        
    }
}
