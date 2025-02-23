
/// <summary>
/// 材料数据类
/// </summary>
public class MaterialData : ItemData
{
    public MaterialData(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite) : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        
    }
    
    public override string SetTipsText()
    {
        string text = base.SetTipsText();
        text += string.Format("\n<color=red>类型：{0}</color>","材料");
        return text;
    }
}
