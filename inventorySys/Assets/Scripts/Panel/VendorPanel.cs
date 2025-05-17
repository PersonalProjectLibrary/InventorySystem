
using UnityEngine;

public class VendorPanel : InventoryPanel
{
    #region 单例模式
    private static VendorPanel _instance;
    public static VendorPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("VendorPanel").GetComponent<VendorPanel>();
            }
            return _instance;
        }
    }
    #endregion

    protected override void Init()
    {
        slotCount = Constant.VendorSlotCount;
        base.Init();
    }
    protected override void InstanticeSlot()
    {
        foreach(var good in Constant.VendorIdName)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotGo.name = Constant.VendorIdName[good.Key];
            slotList.Add(slotGo.GetComponent<VendorSlot>());
            ItemData itemData = inventoryMgr.GetItemDataById(good.Key);
            VendorSlot slot = slotList[slotList.Count-1] as VendorSlot;
            slot.slotName.text = itemData.Name;
            slot.StoreItem(itemData);
            slot.item.itemCountText.text = $"${slot.item.selfData.BuyPrice}";
        }
    }
    public void BuyItem(ItemData item)//商店格子被右键时执行，购买物品
    {
        if(player.ConsumeCoin(item.BuyPrice))
        {
            inventoryMgr.knapsackPanel.ObtainItem(item);
        }
    }
    public void SellItem()//出售鼠标上的物品
    {
        Item item = inventoryMgr.PickedItem;
        int sellCount =Input.GetKey(KeyCode.LeftShift)? 1 :item.ItemAmount;
        int sellPrice = item.selfData.SellPrice*sellCount;
        player.EarnCoin(sellPrice);
        inventoryMgr.UpdateMousePickedCount(item.ItemAmount-sellCount);
    }
}
