
using UnityEngine;

public class ChestPanel : Inventory
{
    #region 单例模式
    private static ChestPanel _instance;
    public static ChestPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ChestPanel").GetComponent<ChestPanel>();
            }
            return _instance;
        }
    }
    #endregion

    protected override void Init()
    {
        slotCount = Constant.ChestSlotCount;
        base.Init();
    }
}
