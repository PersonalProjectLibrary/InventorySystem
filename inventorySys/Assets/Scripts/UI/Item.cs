
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
        Sprite sprite = Resources.Load<Sprite>(selfData.Sprite);
        GetComponent<Image>().sprite = sprite;
        UpdateItemCount(1);
        //Debug.Log("物品初始化成功");
    }
    public void UpdateItemCount(int count)
    {
        ItemCount = count;
        itemCountText.text = count.ToString();
        //Debug.Log("物品数量更新成功");
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
