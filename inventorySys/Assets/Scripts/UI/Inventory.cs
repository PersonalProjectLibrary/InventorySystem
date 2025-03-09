
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包基类
/// </summary>
public class Inventory:MonoBehaviour
{
    protected InventoryManager inventoryMgr;
    protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            return canvasGroup;
        }
    }

    protected int slotCount = 0;
    public Transform slotParent;
    public GameObject slotPrefab;
    protected List<Slot> slotList=new List<Slot>();
    
    private float targetAlpha;

    protected virtual void InitInventory()
    {
        inventoryMgr = InventoryManager.Instance;
        canvasGroup = GetComponent<CanvasGroup>();
        HideInventory();
    }
    protected void InstanticeSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotList.Add(slotGo.GetComponent<Slot>());
        }
    }

    public void Start()
    {
        InitInventory();
        InstanticeSlot();
    }
    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Constant.AlphaSmoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
    protected void ShowInventory()
    {
        targetAlpha = Constant.ShowAlpha;
        canvasGroup.blocksRaycasts = true;
    }
    protected void HideInventory()
    {
        targetAlpha = Constant.HideAlpha;
        canvasGroup.blocksRaycasts = false;//不接收射线，射线会穿透界面，更不会触发射线检测鼠标点击事件
    }
    public void SwitchDisplay()
    {
        if (canvasGroup.alpha == Constant.ShowAlpha)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
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
            slot.InitStoreItem(itemData);
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
                slot.InitStoreItem(itemData);
            }
            else slot.UpdateItem(slot.item.ItemCount + 1);
        }
        //Debug.Log(itemData.Name + "存储成功");
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
