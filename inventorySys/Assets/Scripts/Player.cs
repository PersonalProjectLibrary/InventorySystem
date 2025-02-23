
using UnityEngine;

/// <summary>
/// 玩家/功能测试类
/// </summary>
public class Player : MonoBehaviour
{


    void Update()
    {
        // 按下G键，给背包添加一个随机物品
        if (Input.GetKeyDown(KeyCode.G))
        {
            InventoryManager.Instance.knapsack.StoreItem(Random.Range(1, 3));
        }
        // 按下T键，给背包添加一个随机装备
        if (Input.GetKeyDown(KeyCode.T))
        {
            InventoryManager.Instance.knapsack.StoreItem(Random.Range(3, 13));
        }
        // 按下U键，给背包添加一个随机武器
        if (Input.GetKeyDown(KeyCode.U))
        {
            InventoryManager.Instance.knapsack.StoreItem(Random.Range(13, 15));
        }
        // 按下I键，给背包添加一个随机材料
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryManager.Instance.knapsack.StoreItem(Random.Range(15, 18));
        }
    }
}
