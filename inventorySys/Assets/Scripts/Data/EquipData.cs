
/// <summary>
/// 装备数据类
/// </summary>
public class EquipData : ItemData
{
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public EquipType EqType { get; set; }

    public EquipData(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite,
        int strength, int intellect, int agility, int stamina, EquipType equipType)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        Strength = strength;
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        EqType = equipType;
    }

    public enum EquipType:int
    {
        Head,
        Neck,
        Chest,
        Ring,
        Leg,
        Bracer,
        Boots,
        Trinket,
        Shoulder,
        Belt,
        OffHand,
    }

    public override string SetTipsText()
    {
        string text = base.SetTipsText();
        string typeName = Constant.EquipmentTypeNames[(int)EqType];
        text += string.Format("\n<color=navy>装备类型：{4}</color>\n<color=red>力量：{0}</color>\n<color=yellow>智力：{1}</color>\n<color=green>敏捷：{2}</color>\n<color=blue>耐力：{3}</color>", Strength, Intellect, Agility, Stamina, typeName);
        return text;
    }
}
