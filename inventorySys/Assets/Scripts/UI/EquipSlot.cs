
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlot : Slot
{
    public EquipData.EquipType eType;
    public WeaponData.WeaponType wType;
    public Text slotName;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(!inventoryMgr.IsPicked)
        {
            if(!IsEmpty())PickItem(1);
        }
        else//有选中物品
        {
            if(IsEmpty())//格子没有物品
            {
                if(IsTypeMatch())StoreItem(1);
            }
            else
            {
                if(IsTypeMatch())
                {
                    if(!IsSame())
                    {
                        if(inventoryMgr.PickedItem.ItemCount == 1)ExchangeItem();
                    }
                }
            }
        }
    }

    private bool IsTypeMatch()
    {
        var data = inventoryMgr.PickedItem.selfData;
        switch (data.Type)
        {
            case ItemData.ItemType.Equipment:
                var equipData = (EquipData)data;
                return equipData.EqType == eType;
            case ItemData.ItemType.Weapon:
                var weaponData = (WeaponData)data;
                return weaponData.WpType == wType;
            default:
                break;
        }
        return false;
    }
}
