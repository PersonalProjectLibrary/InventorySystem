
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包基类
/// </summary>
public class Inventory:MonoBehaviour
{
    protected InventoryManager inventoryMgr;
    
    protected int slotCount = 0;
    public Transform slotParent;
    public GameObject slotPrefab;
    protected List<Slot> slotList=new List<Slot>();

    protected virtual void InitInventory()
    {
        inventoryMgr = InventoryManager.Instance;
    }

    /// <summary>
    /// 初始化背包里的格子
    /// </summary>
    protected void InstanticeSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotList.Add(slotGo.GetComponent<Slot>());
        }
    }
}
