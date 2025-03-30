
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 物品槽
/// </summary>
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    protected InventoryManager inventoryMgr;
    public GameObject itemPrefab;
    protected GameObject itemGo;
    public Item item{ get; set; }
    public void Start()
    {
        inventoryMgr = InventoryManager.Instance;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty())
        {
            inventoryMgr.ShowItemTips(item.selfData.SetTipsText());
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!IsEmpty())
        {
            inventoryMgr.HideItemTips();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            ClickItemByMouseLeft();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            ClickItemByMouseRight();
        }
    }

    protected virtual void ClickItemByMouseLeft()
    {
        if(!IsEmpty())//格子有物品
        {
            if(!inventoryMgr.IsPicked)//没有选中物品
            {
                int pickCount;
                if(Input.GetKey(KeyCode.LeftControl))
                {
                    pickCount = (item.ItemAmount + 1) / 2;
                }
                else pickCount = item.ItemAmount;
                MousePickItem(pickCount);
            }
            else//有选中物品
            {
                if(IsSameItem(inventoryMgr.PickedItem.selfData.ID))
                {
                    if(!IsFilled())
                    {
                        if(Input.GetKey(KeyCode.LeftControl))// 按下了Ctrl键,放下一个物品
                        {
                            inventoryMgr.UpdateMousePickedCount(inventoryMgr.PickedItem.ItemAmount-1);
                            UpdateItemCount(item.ItemAmount+1);
                        }
                        else// 放下所有物品
                        {
                            int totalCount = item.ItemAmount + inventoryMgr.PickedItem.ItemAmount;
                            int slotItemCount = totalCount < item.selfData.Capacity ? totalCount:item.selfData.Capacity;
                            inventoryMgr.UpdateMousePickedCount(totalCount-slotItemCount);
                            UpdateItemCount(slotItemCount);
                        }
                    }
                }
                else ExchangeMouseItem();
            }
        }
        else//格子没有物品
        {
            if(inventoryMgr.IsPicked)//有选中物品
            {
                var item = inventoryMgr.PickedItem;
                if(Input.GetKey(KeyCode.LeftControl))
                {
                    StoreItem(item.selfData);
                    inventoryMgr.UpdateMousePickedCount(item.ItemAmount-1);
                }
                else 
                {
                    StoreItem(item.selfData,item.ItemAmount);
                    inventoryMgr.UpdateMousePickedCount(0);
                }
            }
        }
    }
    protected void ClickItemByMouseRight()
    {
        if(!IsEmpty()&&!inventoryMgr.IsPicked)
        {
            var itemType = item.selfData.Type;
            if(itemType is ItemData.ItemType.Equipment||itemType is ItemData.ItemType.Weapon)
            {
                var data = item.selfData;
                item.UpdateAmount(item.ItemAmount-1);
                if(item.ItemAmount == 0)
                {
                    item = null;
                    //if(itemGo != null)DestroyImmediate(itemGo);//不立即销毁会报错，故不用UpdateItemCount()方法
                    if(itemGo != null)
                    {
                        var tempGo = itemGo;//避免下面置空先于Destory情况，导致内存泄漏
                        Destroy(tempGo);//运行时，不能使用DestroyImmediate
                        itemGo = null;//强制清空
                    }
                    inventoryMgr.HideItemTips();
                }
                GearPanel.Instance.PutOn(data);
            }
        }
    }
    
    public bool IsEmpty()
    {
        return item == null ? true : item.ItemAmount == 0;
    }
    public bool IsFilled()
    {
        return item.ItemAmount >= item.selfData.Capacity;
    }
    public bool IsSameItem(int itemId)
    {
        return item.selfData.ID == itemId;
    }
    
    public void StoreItem(ItemData data, int count = 1)
    {
        if (itemGo == null)// 之前格子里没有物品
        {
            itemGo = Instantiate(itemPrefab, transform);
            itemGo.transform.localPosition = Vector3.zero;
            item = itemGo.GetComponent<Item>();
            item.Init(data);
        }
        itemGo.transform.SetAsFirstSibling();
        UpdateItemCount(count);
    }
    public void UpdateItemCount(int count = 1)
    {
        if(count == 0)ClearItem();
        else item.UpdateAmount(count);
    }
    public virtual void ClearItem()
    {
        item = null;
        if(itemGo != null)Destroy(itemGo);//左键拾取物品，如果使用立即销毁，会行为异常
    }
    
    protected void MousePickItem(int count = 1)
    {
        inventoryMgr.MousePickUpItem(item, count);
        if(item.ItemAmount == count)ClearItem();
        else item.UpdateAmount(item.ItemAmount-count);
    }
    protected void ExchangeMouseItem()
    {
        ItemData tempData = item.selfData;
        int tempCount = item.ItemAmount;
        item.Init(inventoryMgr.PickedItem.selfData);
        item.UpdateAmount(inventoryMgr.PickedItem.ItemAmount);
        inventoryMgr.PickedItem.Init(tempData);
        inventoryMgr.PickedItem.UpdateAmount(tempCount);
    }
    
}
