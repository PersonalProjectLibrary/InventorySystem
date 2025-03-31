
using UnityEngine;

public class GearPanel : Inventory
{
    #region 单例模式
    private static GearPanel _instance;
    public static GearPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GearPanel").GetComponent<GearPanel>();
            }
            return _instance;
        }
    }
    #endregion

    protected override void Init()
    {
        slotCount = Constant.GearSlotCount;
        base.Init();
    }
    protected override void InstanticeSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotList.Add(slotGo.GetComponent<GearSlot>());
            GearSlot slot = slotList[i] as GearSlot;
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

    public void GearPanelPutOn(ItemData data)
    {
        ItemData itemData = null;
        foreach (var slot in slotList)
        {
            var gs = slot as GearSlot;
            if (gs.IsItemAndSlotMatch(data))
            {
                if(!gs.IsEmpty())
                {
                    itemData = gs.item.selfData;
                    gs.ClearItem();
                }
                gs.StoreItem(data);
                break;
            }
        }
        if (itemData != null)
        {
            KnapsackPanel.Instance.ObtainItem(itemData);
        }
    }
    public void GearPanelPutOff(ItemData data)
    {
        KnapsackPanel.Instance.ObtainItem(data);
    }
}
