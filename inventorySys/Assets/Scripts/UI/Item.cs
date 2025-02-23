
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

    public ItemData.ItemType GetItemType()
    {
        return selfData.Type;
    }
}
