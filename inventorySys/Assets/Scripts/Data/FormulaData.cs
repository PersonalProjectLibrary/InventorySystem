
using System.Collections.Generic;

public class FormulaData
{
    public int Item1Id{get; private set;}//配方/材料物品1
    public int Item1Count{get; private set;}
    public int Item2Id{get;private set;}//配方/材料物品2
    public int Item2Count{get;private set;}
    public int ResultItemId{get;private set;}//锻造出来的物品
    public Dictionary<int,int> needMaterials{get;private set;}//该秘籍需要的[物品,数量]
    public FormulaData(int item1Id,int item1Count,int item2Id,int item2Count,int resultItemId)
    {
        Item1Id = item1Id;
        Item1Count = item1Count;
        Item2Id = item2Id;
        Item2Count = item2Count;
        ResultItemId = resultItemId;
        needMaterials = new Dictionary<int,int>()
        {
            {Item1Id,Item1Count},
            {Item2Id,Item2Count}
        };
    }

    /// <summary>
    /// 判断拥有的材料数量是否能满足这个配方
    /// </summary>
    /// <param name="hadMaterials"></param>
    /// <returns></returns>
    public bool IsMatch(Dictionary<int,int> hadMaterials)
    {
        foreach (var item in needMaterials)
        {
            if(!hadMaterials.ContainsKey(item.Key))return false;
            else
            {
                if(hadMaterials[item.Key] < item.Value)return false;
            }
        }
        return true;
    }
}
