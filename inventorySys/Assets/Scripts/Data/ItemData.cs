
/// <summary>
/// 物品数据基类
/// </summary>
public class ItemData
{
    public int ID{get; set;}
    public string Name{get; set;}
    public ItemType Type{get; set;}
    public ItemQuality Quality{get; set;}
    public string Description{get; set;}
    public int Capacity{get; set;}
    public int BuyPrice{get; set;}
    public int SellPrice{get; set;}
    public string Sprite{get; set;}

    public ItemData()
    {
        ID = -1;
    }

    public ItemData(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite)
    {
        ID = id;
        Name = name;
        Type = type;
        Quality = quality;
        Description = des;
        Capacity = capacity;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Sprite = sprite;
    }

    public enum ItemType//物品类型
    {
        Consumable,
        Equipment,
        Weapon,
        Material
    }

    public enum ItemQuality//物品品质
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artifact
    }
}
