using System.Collections;
using System.Collections.Generic;
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
            slot.equipName.text = Constant.EquipmentTypeNames[i];
            slotGo.name = $"{slot.equipType}Slot";
        }
    }
}
