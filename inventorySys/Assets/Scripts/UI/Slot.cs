
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
    private int itemCount = 0;

    public void Start()
    {
        inventoryMgr = InventoryManager.Instance;
    }

    public bool IsEmpty()
    {
        return itemCount == 0;
    }
    public void StoreItem(ItemData data)
    {
        if (itemGo == null)// 之前格子里没有物品
        {
            itemGo = Instantiate(itemPrefab, transform);
            itemGo.transform.localPosition = Vector3.zero;
            item = itemGo.GetComponent<Item>();
            item.InitItem(data);
        }
        StoreItem();
    }
    public void StoreItem()
    {
        itemCount++;
        item.UpdateItem(itemCount);
    }
    public void RemoveItem()
    {
        itemCount--;
        if(itemCount == 0)Destroy(itemGo);
        else item.UpdateItem(itemCount);
    }
    public void ClearSlot()
    {
        itemCount = 0;
        item = null;
        if(itemGo != null)Destroy(itemGo);
    }
    public bool IsFilled()
    {
        return itemCount >= item.selfData.Capacity;
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
                if(Input.GetKey(KeyCode.LeftControl))// 按下了Ctrl键
                {
                }
                else
                {
                    //将当前物品放到鼠标上
                    inventoryMgr.PickUpItem(item, itemCount);
                    //清空当前格子
                    ClearSlot();
                }
            }
        }
    }
}
