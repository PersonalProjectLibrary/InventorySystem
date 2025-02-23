
using UnityEngine;

/// <summary>
/// 常量类
/// </summary>
public class Constant
{
    /// <summary>
    /// 背包格子数量
    /// </summary>
    public static int KnapsackSlotCount = 20;

    /// <summary>
    /// 提示信息透明度平滑度
    /// </summary>
    public static float TipsAlphaSmoothing = 5f;

    public static Vector2 TipsPosOffset = new Vector2(45, 45); 

    //物品信息
    public static string[] ItemTypeNames = { "消耗品", "装备","武器", "材料" };
    public static string[] QualityNames = { "普通", "非凡", "稀有", "史诗", "传说", "神器" };
    public static string[] QualityColors = { "white", "lime", "navy", "magenta", "orange", "red" };
    public static string[] WeaponTypeNames = { "副手", "主手"};
    //public static string[] EquipmentTypeNames = { "头盔","项链", "胸甲", "戒指","护腿", "护腕", "靴子","饰品","护肩", "腰带", "副手" };

}
