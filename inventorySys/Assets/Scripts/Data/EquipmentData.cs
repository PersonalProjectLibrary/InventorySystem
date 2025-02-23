
/// <summary>
/// 装备数据类
/// </summary>
public class EquipmentData : ItemData
{
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public EquipmentType EquipType { get; set; }

    public EquipmentData(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite,
        int strength, int intellect, int agility, int stamina, EquipmentType equipType)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        Strength = strength;
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        EquipType = equipType;
    }

    public enum EquipmentType
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
        OffHand
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        //string equipType = Constant.EquipmentTypeNames[(int)EquipType];
        //text += string.Format("\n<color=red>力量：{0}</color>\n<color=red>智力：{1}</color>\n<color=red>敏捷：{2}</color>\n<color=red>耐力：{3}</color>\n<color=yellow>类型：{4}</color>", Strength, Intellect, Agility, Stamina, equipType);
        text += string.Format("\n<color=red>力量：{0}</color>\n<color=red>智力：{1}</color>\n<color=red>敏捷：{2}</color>\n<color=red>耐力：{3}</color>", Strength, Intellect, Agility, Stamina);
        return text;
    }
}
