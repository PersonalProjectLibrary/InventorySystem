
/// <summary>
/// 武器类
/// </summary>
public class Weapon : Item
{
    public int Damage { get; set; }
    public WeaponType WpType { get; set; }

    public Weapon(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, 
        int damage, WeaponType weaponType): base(id, name, type, quality, des, capacity, buyPrice, sellPrice)
    {
        Damage = damage;
        WpType = weaponType;
    }

    public enum WeaponType
    {
        OffHand,
        MainHand
    }
}
