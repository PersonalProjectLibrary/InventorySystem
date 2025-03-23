
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
        UpdateItem(1);
    }
    public void UpdateItem(int count)
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
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(!IsEmpty())//当前格子有物品
        {
            if(!inventoryMgr.IsPicked)PickItem();
            else//当前有选中物品
            {
                if(IsSame())
                {
                    if(!IsFilled())DropItem();
                }
                else ExchangeItem();
            }
        }
        else//当前格子没有物品
        {
            if(inventoryMgr.IsPicked)StoreItem();
        }
    }
    
    protected void StoreItem(int count)
    {
        InitStoreItem(inventoryMgr.PickedItem.selfData);// 初始化格子，默认存一个物品
        if(count!=1)item.UpdateItemCount(count);//更新为指定个数
        inventoryMgr.UpdatePickedItem(inventoryMgr.PickedItem.ItemCount-count);
    }
    private void StoreItem()
    {
        if(Input.GetKey(KeyCode.LeftControl))// 按下了Ctrl键，存储一个物品
        {
            StoreItem(1);
        }
        else// 存储所有物品
        {
            StoreItem(inventoryMgr.PickedItem.ItemCount);
        }
    }

    protected void PickItem(int count)
    {
        inventoryMgr.PickUpItem(item, count);
        if(item.ItemCount == count)ClearItem();
        else item.UpdateItemCount(item.ItemCount-count);
    }
    private void PickItem()
    {
        int pickCount;
        if(Input.GetKey(KeyCode.LeftControl))// 按下了Ctrl键，取一半数量的物品
        {
            pickCount = (item.ItemCount + 1) / 2;
        }
        else// 没有按下Ctrl键，取全部物品
        {
            pickCount = item.ItemCount;
        }
        PickItem(pickCount);
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
