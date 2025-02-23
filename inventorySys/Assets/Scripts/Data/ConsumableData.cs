
/// <summary>
/// 消耗品数据类
/// </summary>
public class ConsumableData : ItemData
{
    public int Hp { get; set; }
    public int Mp { get; set; }

    public ConsumableData(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice,string sprite, 
    int hp, int mp) : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        Hp = hp;
        Mp = mp;
    }
    public override string SetTipsText()
    {
        string text = base.SetTipsText();
        text += string.Format("\n回复生命值：{0}\n回复魔法值：{1}",Hp, Mp);
        return text;
    }
}
