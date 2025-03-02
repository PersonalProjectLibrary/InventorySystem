
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
            InventoryManager.Instance.ShowItemTips(item.selfData.SetTipsText());
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!IsEmpty())
        {
            InventoryManager.Instance.HideItemTips();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!IsEmpty())//当前格子有物品
        {
            if(!InventoryManager.Instance.IsPickedItem)//当前没有选中物品
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
        }
    }
}
