using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 常量类
/// </summary>
public class Constant
{
    // 面板显示掩藏设置
    public static float ShowAlpha =1;
    public static float HideAlpha = 0;
    public static float AlphaSmoothing = 5f;

    // 提示面板与目标位置的偏移量
    public static Vector2 TipsPosOffset = new Vector2(45, 45); 

    // 背包格子数量
    public static int KnapsackSlotCount = 16;
    public static int ChestSlotCount = 8;
    public static int GearSlotCount = 11;
    public static int VendorSlotCount = 12;

    //物品信息
    public static string[] ItemTypeNames = { "消耗品", "装备","武器", "材料" };
    public static string[] QualityNames = { "普通", "非凡", "稀有", "史诗", "传说", "神器" };
    public static string[] QualityColors = { "white", "lime", "navy", "magenta", "orange", "red" };
    public static string[] WeaponTypeNames = { "空武器","副手", "主手"};
    public static string[] EquipTypeNames = { "空装备","头盔","项链","护肩", "胸甲", "戒指","护腕", "腰带", "副手", "护腿", "靴子" };
    public static Dictionary<int,string> VendorIdName = new Dictionary<int, string>()
    {
        {1,"hp"},{2,"mp"},{3,"armor"},{5,"boots"},{6,"bracers"},{7,"gloves"},
        {8,"helmets"},{9,"necklace"},{10,"rings"},{15,"book"},{16,"scroll"},{17,"ingots"},
    };
    //物品大小动画数值
    public static Vector3 ItemDefaultScale = new Vector3(1, 1, 1);
    public static Vector3 ItemAnimationScale = new Vector3(1.5f, 1.5f, 1.5f);
    public static float ItemAnimationScaleSpeed = 5f;

}
