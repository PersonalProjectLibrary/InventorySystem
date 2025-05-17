
using UnityEngine;
using UnityEngine.UI;

public class GearPanel : InventoryPanel
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
    
    public Text propertyText;

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
        UpdateProperty();
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
                UpdateProperty();
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
        UpdateProperty();
    }

    public void UpdateProperty()
    {
        int strength = 0, intellect = 0, agility = 0, stamina = 0, damage = 0;
        foreach (GearSlot slot in slotList)
        {
            if (slot.IsEmpty())continue;
            if(slot.item.selfData is WeaponData)
            {
                WeaponData data = slot.item.selfData as WeaponData;
                data.GetProperty(ref damage);
            }
            else
            {
                EquipData data = slot.item.selfData as EquipData;
                data.GetProperty(ref strength, ref intellect, ref agility, ref stamina);
            }
        }
        player.AddBasicProperty(ref strength, ref intellect, ref agility, ref stamina, ref damage);
        propertyText.text = $"力量：{strength}\n智力：{intellect}\n敏捷：{agility}\n耐力：{stamina}\n攻击力：{damage}";
    }
}
