
using UnityEngine;

/// <summary>
/// 物品槽
/// </summary>
public class Slot:MonoBehaviour
{
    public GameObject itemPrefab;
    private GameObject itemGo;
    public Item item{ get; set; }
    private int itemCount = 0;

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

    public bool IsFilled()
    {
        return itemCount >= item.selfData.Capacity;
    }
}
