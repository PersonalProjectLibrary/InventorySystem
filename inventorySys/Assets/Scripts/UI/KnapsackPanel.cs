
using UnityEngine;

/// <summary>
/// 背包
/// </summary>
public class KnapsackPanel : Inventory
{
    #region 单例模式
    private static KnapsackPanel _instance;
    public static KnapsackPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapsackPanel").GetComponent<KnapsackPanel>();
            }
            return _instance;
        }
    }
    #endregion

    protected override void Init()
    {
        slotCount = Constant.KnapsackSlotCount;
        base.Init();
    }
}
