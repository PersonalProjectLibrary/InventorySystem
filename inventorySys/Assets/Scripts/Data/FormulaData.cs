
public class FormulaData
{
    public int Item1Id{get; private set;}//配方/材料物品1
    public int Item1Count{get; private set;}
    public int Item2Id{get;private set;}//配方/材料物品2
    public int Item2Count{get;private set;}
    public int ResultItemId{get;private set;}//锻造出来的物品
    public FormulaData(int item1Id,int item1Count,int item2Id,int item2Count,int resultItemId)
    {
        Item1Id = item1Id;
        Item1Count = item1Count;
        Item2Id = item2Id;
        Item2Count = item2Count;
        ResultItemId = resultItemId;
    }
}
