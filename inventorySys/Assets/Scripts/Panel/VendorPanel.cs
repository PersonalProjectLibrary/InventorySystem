
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
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotList.Add(slotGo.GetComponent<VendorSlot>());
            VendorSlot slot = slotList[i] as VendorSlot;

            int itemId = Constant.VendorId[i];
            ItemData itemData = inventoryMgr.GetItemDataById(itemId);
            slot.slotName.text = itemData.Name;
            slotGo.name = string.Format($"{itemData.Name}Slot");
            slot.StoreItem(itemData);
            slot.item.itemCountText.gameObject.SetActive(false);
        }
    }
}
