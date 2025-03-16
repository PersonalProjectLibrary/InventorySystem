
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
    private  EquipSlot mainHandSlot;
    private  EquipSlot offHandSlot;

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
            slot.equipType = (EquipData.EquipType)i;
            if(i == 0)
            {
                slot.equipName.text = "武器";
                slotGo.name = "WeaponSlot";
                mainHandSlot = slot;
            }
            else
            {
                slot.equipName.text = Constant.EquipTypeNames[i];
                slotGo.name = $"{slot.equipType}Slot";
                if((EquipData.EquipType)i == EquipData.EquipType.OffHand)offHandSlot = slot;
            }
            
        }
    }
}
