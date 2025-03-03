
public class Chest : Inventory
{
    protected override void InitInventory()
    {
        base.InitInventory();
        slotCount = Constant.ChestSlotCount;
        InstanticeSlot();
    }

    private void Start()
    {
        InitInventory();
    }
}
