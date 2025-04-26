
using UnityEngine;

public class ForgePanel : InventoryPanel
{
    #region 单例模式
    private static ForgePanel _instance;
    public static ForgePanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ForgePanel").GetComponent<ForgePanel>();
            }
            return _instance;
        }
    }
    #endregion

    protected override void Init()
    {
        slotCount = Constant.ForgeSlotCount;
        base.Init();
    }
    protected override void InstanticeSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotGo.name = $"Slot{i+1}";
            slotList.Add(slotGo.GetComponent<ForgeSlot>());
        }
    }
}
