
using UnityEngine;

public class EquipBag : Inventory
{
    #region 单例模式
    private static EquipBag _instance;
    public static EquipBag Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("EquipPanel").GetComponent<EquipBag>();
            }
            return _instance;
        }
    }
    #endregion

    //武器存放位置

    protected override void InitInventory()
    {
        slotCount = Constant.EquipSlotCount;
        base.InitInventory();
    }

    protected override void InstanticeSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotList.Add(slotGo.GetComponent<EquipSlot>());
            EquipSlot slot = slotList[i] as EquipSlot;
            slot.eType = (EquipData.EquipType)i;
            if(i == 0)
            {
                slot.slotName.text = "武器";
                slotGo.name = "WeaponSlot";
            }
            else
            {
                slot.slotName.text = Constant.EquipTypeNames[i];
                slotGo.name = $"{slot.eType}Slot";
            }
            if(slot.eType == EquipData.EquipType.None)slot.wType = WeaponData.WeaponType.MainHand;
            else if(slot.eType==EquipData.EquipType.OffHand)slot.wType = WeaponData.WeaponType.OffHand;
            else slot.wType = WeaponData.WeaponType.None;
        }
    }
}
