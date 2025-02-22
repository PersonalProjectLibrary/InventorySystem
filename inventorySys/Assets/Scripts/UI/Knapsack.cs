
/// <summary>
/// 背包
/// </summary>
public class Knapsack : Inventory
{
    protected override void InitInventory()
    {
        base.InitInventory();
        slotCount = Constant.KnapsackSlotCount;
        InstanticeSlot();
    }

    private void Start()
    {
        InitInventory();
    }
}
