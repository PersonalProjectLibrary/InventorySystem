
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品
/// </summary>
public class Item : MonoBehaviour
{
    public Text itemCountText;
    public ItemData selfData{ get; set; }
    public int ItemCount{ get; set; }
    
    public void InitItem(ItemData data )
    {
        selfData = data;
        ItemCount = 1;
        itemCountText.text = ItemCount.ToString();
        Sprite sprite = Resources.Load<Sprite>(selfData.Sprite);
        GetComponent<Image>().sprite = sprite;
        //Debug.Log("物品初始化成功");
    }

    public void UpdateItem(int count)
    {
        ItemCount = count;
        itemCountText.text = count.ToString();
        //Debug.Log("物品数量更新成功");
    }
    public void ExChangeItem(Item item)
    {
        ItemData tempData = item.selfData;
        int tempCount = item.ItemCount;
        item.InitItem(selfData);
        item.UpdateItem(ItemCount);
        
        InitItem(tempData);
        UpdateItem(tempCount);
        //Debug.Log("物品交换成功");
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
