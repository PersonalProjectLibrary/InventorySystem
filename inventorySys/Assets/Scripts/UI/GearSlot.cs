
using UnityEngine.UI;

public class GearSlot : Slot
{
    public EquipData.EquipType eType;
    public WeaponData.WeaponType wType;
    public Text slotName;

    protected override void ClickItemByMouseLeft()
    {
        if(!inventoryMgr.IsPicked)
        {
            if(!IsEmpty())
            {
                MousePickItem();
            }
        }
        else//有选中物品
        {
            var data = inventoryMgr.PickedItem.selfData;
            int itemCount = inventoryMgr.PickedItem.ItemAmount;
            if(IsEmpty())//格子没有物品
            {
                if(IsItemAndSlotMatch(data))
                {
                    StoreItem(data);
                    inventoryMgr.UpdateMousePickedCount(itemCount-1);
                }
            }
            else
            {
                if(IsItemAndSlotMatch(data))
                {
                    if(!IsSameItem(data.ID))
                    {
                        if(itemCount == 1)
                        {
                            ExchangeMouseItem();
                        }
                    }
                }
            }
        }
    }

    public bool IsItemAndSlotMatch(ItemData data)
    {
        switch (data.Type)
        {
            case ItemData.ItemType.Equipment:
                var equipData = (EquipData)data;
                return equipData.EqType == eType;
            case ItemData.ItemType.Weapon:
                var weaponData = (WeaponData)data;
                return weaponData.WpType == wType;
        }
        return false;
    }

    public override void ClearItem()
    {
        item = null;
        //if(itemGo != null)DestroyImmediate(itemGo);//不立即销毁会报错
        if(itemGo != null)
        {
            var tempGo = itemGo;//避免下面置空先于Destory情况，导致内存泄漏
            Destroy(tempGo);//运行时，不能使用DestroyImmediate
            itemGo = null;//强制清空
        }
    }
}
