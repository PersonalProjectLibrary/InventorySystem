
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
        // 背包显示时，按下K、C、G键，显示/隐藏背包、箱子、装备
        if(Input.GetKeyDown(KeyCode.K))
        {
            inventoryMgr.knapsackPanel.SwitchDisplayState();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            inventoryMgr.chestPanel.SwitchDisplayState();
        }
        else if(Input.GetKeyDown(KeyCode.G))
        {
            inventoryMgr.gearPanel.SwitchDisplayState();
        }

        // 背包显示时，按下I、E、W、M键，给背包添加一个随机物品
        if(inventoryMgr.knapsackPanel.CanvasGroup.alpha==Constant.ShowAlpha)
        {
            if (Input.GetKeyDown(KeyCode.I))//添加随机物品
            {
                inventoryMgr.knapsackPanel.ObtainItem(Random.Range(1, 3));
            }
            else if (Input.GetKeyDown(KeyCode.E))//添加随机装备
            {
                inventoryMgr.knapsackPanel.ObtainItem(Random.Range(3, 13));
            }
            else if (Input.GetKeyDown(KeyCode.W))//添加随机武器
            {
                inventoryMgr.knapsackPanel.ObtainItem(Random.Range(13, 15));
            }
            else if (Input.GetKeyDown(KeyCode.M))//添加随机材料
            {
                inventoryMgr.knapsackPanel.ObtainItem(Random.Range(15, 18));
            }
        }
    }
}
