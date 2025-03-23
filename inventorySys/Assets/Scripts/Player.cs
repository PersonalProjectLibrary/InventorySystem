
using UnityEngine;

/// <summary>
/// 玩家/功能测试类
/// </summary>
public class Player : MonoBehaviour
{
    private InventoryManager inventoryMgr
    {
        get
        {
            return InventoryManager.Instance;
        }
    }

    void Update()
    {
        // 背包显示时，按下K、C、B键，显示/隐藏背包、箱子
        if(Input.GetKeyDown(KeyCode.K))
        {
            inventoryMgr.knapsack.SwitchDisplay();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            inventoryMgr.chest.SwitchDisplay();
        }
        else if(Input.GetKeyDown(KeyCode.B))// 显示/隐藏装备栏
        {
            inventoryMgr.equipBag.SwitchDisplay();
        }

        // 背包显示时，按下I、E、W、M键，给背包添加一个随机物品
        if(inventoryMgr.knapsack.CanvasGroup.alpha==Constant.ShowAlpha)
        {
            if (Input.GetKeyDown(KeyCode.I))//添加随机物品
            {
                inventoryMgr.knapsack.StoreItem(Random.Range(1, 3));
            }
            else if (Input.GetKeyDown(KeyCode.E))//添加随机装备
            {
                inventoryMgr.knapsack.StoreItem(Random.Range(3, 13));
            }
            else if (Input.GetKeyDown(KeyCode.W))//添加随机武器
            {
                inventoryMgr.knapsack.StoreItem(Random.Range(13, 15));
            }
            else if (Input.GetKeyDown(KeyCode.M))//添加随机材料
            {
                inventoryMgr.knapsack.StoreItem(Random.Range(15, 18));
            }
        }
    }
}
