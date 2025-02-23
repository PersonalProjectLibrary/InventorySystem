
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
    protected void InstanticeSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotList.Add(slotGo.GetComponent<Slot>());
        }
    }

    public bool StoreItem(int itemId)
    {
        ItemData itemData = inventoryMgr.GetItemDataById(itemId);
        return StoreItem(itemData);
    }
    public bool StoreItem(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.LogError("要存储的物品不存在");
            return false;
        }

        Slot slot;
        if(itemData.Capacity == 1)// 物品容量为1，不能和其他同类物品放一起
        {
            slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.Log("没有空的物品槽");
                return false;
            }
            slot.StoreItem(itemData);
        }
        else
        {
            slot = FindSameItemSlot(itemData);
            if(slot == null)//没有存储过该物品的格子
            {
                slot = FindEmptySlot();
                if (slot == null)
                {
                    Debug.Log("没有空的物品槽");
                    return false;
                }
                slot.StoreItem(itemData);
            }
            else slot.StoreItem();
        }
        Debug.Log(itemData.Name + "存储成功");
        return true;
    }

    protected Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            // 如果格子是空的
            if (slot.IsEmpty())
            {
                return slot;
            }
        }
        return null;
    }

    protected Slot FindSameItemSlot(ItemData itemData)
    {
        foreach (Slot slot in slotList)
        {
            // 如果格子里有物品，并且物品ID相同，并且物品数量没有达到上限
            if (slot.item != null && slot.item.selfData.ID == itemData.ID && !slot.IsFilled())
            {
                return slot;
            }
        }
        return null;
    }
}
