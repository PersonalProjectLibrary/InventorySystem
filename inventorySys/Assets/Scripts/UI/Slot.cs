
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 物品槽
/// </summary>
public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    protected InventoryManager inventoryMgr;
    public GameObject itemPrefab;
    private GameObject itemGo;
    public Item item{ get; set; }
    public void Start()
    {
        inventoryMgr = InventoryManager.Instance;
    }

    public bool IsEmpty()
    {
        return item==null?true:item.ItemCount==0;
    }
    protected bool IsSame()
    {
        return item.selfData.ID == inventoryMgr.PickedItem.selfData.ID;
    }
    public bool IsFilled()
    {
        return item.ItemCount >= item.selfData.Capacity;
    }

    public void InitStoreItem(ItemData data)
    {
        if (itemGo == null)// 之前格子里没有物品
        {
            itemGo = Instantiate(itemPrefab, transform);
            itemGo.transform.localPosition = Vector3.zero;
            item = itemGo.GetComponent<Item>();
            item.InitItem(data);
        }
        UpdateItem();
    }
    public void UpdateItem(int count = 1)
    {
        item.UpdateItemCount(count);
        if(item.ItemCount == 0)ClearItem();
    }
    public void ClearItem()
    {
        item = null;
        if(itemGo != null)Destroy(itemGo);
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
                    pickCount = (item.ItemCount + 1) / 2;
                }
                else pickCount = item.ItemCount;
                PickItem(pickCount);
            }
            else//有选中物品
            {
                if(IsSame())
                {
                    if(!IsFilled())DropItem();
                }
                else ExchangeItem();
            }
        }
        else//格子没有物品
        {
            if(inventoryMgr.IsPicked)//有选中物品
            {
                if(Input.GetKey(KeyCode.LeftControl))StoreItem();
                else StoreItem(inventoryMgr.PickedItem.ItemCount);
            }
        }
    }
    protected virtual void ClickItemByMouseRight()
    {
    }
    
    
    protected void StoreItem(int count = 1)
    {
        InitStoreItem(inventoryMgr.PickedItem.selfData);// 初始化格子，默认存一个物品
        if(count!=1)item.UpdateItemCount(count);//更新为指定个数
        inventoryMgr.UpdatePickedItem(inventoryMgr.PickedItem.ItemCount-count);
    }

    protected void PickItem(int count = 1)
    {
        inventoryMgr.PickUpItem(item, count);
        if(item.ItemCount == count)ClearItem();
        else item.UpdateItemCount(item.ItemCount-count);
    }
    
    private void DropItem()
    {
        if(Input.GetKey(KeyCode.LeftControl))// 按下了Ctrl键,放下一个物品
        {
            inventoryMgr.UpdatePickedItem(inventoryMgr.PickedItem.ItemCount-1);
            UpdateItem(item.ItemCount+1);
        }
        else// 放下所有物品
        {
            int totalCount = item.ItemCount + inventoryMgr.PickedItem.ItemCount;
            int slotItemCount = totalCount < item.selfData.Capacity ? totalCount:item.selfData.Capacity;
            inventoryMgr.UpdatePickedItem(totalCount-slotItemCount);
            UpdateItem(slotItemCount);
        }
    }

    protected void ExchangeItem()
    {
        ItemData tempData = item.selfData;
        int tempCount = item.ItemCount;
        item.InitItem(inventoryMgr.PickedItem.selfData);
        item.UpdateItemCount(inventoryMgr.PickedItem.ItemCount);
        inventoryMgr.PickedItem.InitItem(tempData);
        inventoryMgr.PickedItem.UpdateItemCount(tempCount);
    }
}
