
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
    }
}
