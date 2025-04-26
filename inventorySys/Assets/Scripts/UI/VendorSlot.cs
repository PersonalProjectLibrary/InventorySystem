
using UnityEngine.UI;

public class VendorSlot : Slot
{
    public Text slotName;

    protected override void ClickItemByMouseLeft()//进行出售
    {
        if(inventoryMgr.IsPicked)//手上东西拖进商店即可出售
        {
            transform.parent.parent.SendMessage("SellItem");
        }
    }
    protected override void ClickItemByMouseRight()//进行购买
    {
        if(!IsEmpty()&&!inventoryMgr.IsPicked)
        {
            transform.parent.parent.SendMessage("BuyItem", item.selfData);
        }
    }
}
