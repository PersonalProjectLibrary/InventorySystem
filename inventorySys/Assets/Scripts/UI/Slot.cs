
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 物品槽
/// </summary>
public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    private InventoryManager inventoryMgr;
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
    public bool IsFilled()
    {
        return item.ItemCount >= item.selfData.Capacity;
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
        if(!IsEmpty())//当前格子有物品
        {
            if(!inventoryMgr.IsPickedItem)//当前没有选中物品
            {
                PickItem();
            }
            else//当前有选中物品
            {
                if(item.selfData.ID == inventoryMgr.PickedItem.selfData.ID)//当前选中物品和当前格子物品相同
                {
                    if(!IsFilled())//当前格子物品未满
                    {
                        DropPickedItem();
                    }
                }
                else//当前选中物品和当前格子物品不同，进行交换
                {
                    ExchangePickedItem();
                }
            }
        }
        else//当前格子没有物品
        {
            if(inventoryMgr.IsPickedItem)//当前有选中物品
            {
                StorePickedItem();
            }
        }
    }
    private void PickItem()// 拾取物品
    {
        if(Input.GetKey(KeyCode.LeftControl))// 按下了Ctrl键，取一半数量的物品
        {
            int pickCount = (item.ItemCount + 1) / 2;
            inventoryMgr.PickUpItem(item, pickCount);

            if(item.ItemCount == pickCount)ClearItem();
            else item.UpdateItemCount(item.ItemCount-pickCount);
        }
        else// 没有按下Ctrl键，取全部物品
        {
            inventoryMgr.PickUpItem(item, item.ItemCount);
            ClearItem();
        }
    }
    private void ExchangePickedItem()// 交换物品
    {
        ItemData tempData = item.selfData;
        int tempCount = item.ItemCount;
        item.InitItem(inventoryMgr.PickedItem.selfData);
        item.UpdateItemCount(inventoryMgr.PickedItem.ItemCount);
        inventoryMgr.PickedItem.InitItem(tempData);
        inventoryMgr.PickedItem.UpdateItemCount(tempCount);
    }
    private void DropPickedItem()// 放下物品
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
    private void StorePickedItem()// 存储选中物品
    {
        InitStoreItem(inventoryMgr.PickedItem.selfData);// 初始化格子，默认存一个物品
        if(Input.GetKey(KeyCode.LeftControl))// 按下了Ctrl键，存储一个物品
        {
            inventoryMgr.UpdatePickedItem(inventoryMgr.PickedItem.ItemCount-1);
        }
        else// 存储所有物品
        {
            item.UpdateItemCount(inventoryMgr.PickedItem.ItemCount);
            inventoryMgr.UpdatePickedItem(0);
        }
    }

}
