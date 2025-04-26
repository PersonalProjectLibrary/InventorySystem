
/// <summary>
/// 武器数据类
/// </summary>
public class WeaponData : ItemData
{
    public int Damage { get; set; }
    public WeaponType WpType { get; set; }

    public WeaponData(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite,
        int damage, WeaponType weaponType): base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        Damage = damage;
        WpType = weaponType;
    }

    public enum WeaponType
    {
        None,
        OffHand,
        MainHand,
    }

    public override string SetTipsText()
    {
        string text = base.SetTipsText();
        string wpType = Constant.WeaponTypeNames[(int)WpType];
        text += string.Format("\n<color=lime>武器类型：{1}</color>\n<color=red>伤害：{0}</color>",Damage, wpType);
        return text;
    }

    public void GetProperty(ref int damage)
    {
        damage += Damage;
    }
}
