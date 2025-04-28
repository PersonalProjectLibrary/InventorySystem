
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品
/// </summary>
public class Item : MonoBehaviour
{
    public Text itemCountText;
    public ItemData selfData{ get; private set; }
    public int ItemAmount{ get; private set; }

    private Vector3 targetScale = Constant.ItemDefaultScale;
    
    public void Init(ItemData data )
    {
        selfData = data;
        Sprite sprite = Resources.Load<Sprite>(selfData.Sprite);
        GetComponent<Image>().sprite = sprite;
        UpdateAmount();
    }
    public void UpdateAmount(int amount = 1)
    {
        ItemAmount = amount;
        itemCountText.text = amount.ToString();
        transform.localScale = Constant.ItemAnimationScale;
    }

    private void Update()
    {
        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Constant.ItemAnimationScaleSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
            }
        }
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
