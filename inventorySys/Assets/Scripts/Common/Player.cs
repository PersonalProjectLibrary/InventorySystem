
using UnityEngine;
using UnityEngine.UI;

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
    public BasicProperty basicProperty = new BasicProperty();
    private int coinAmount = 100;
    private Text coinText;

    void Start()
    {
        coinText = GameObject.Find("Coin").GetComponentInChildren<Text>();
        UpdateCoin();
    }
    void Update()
    {
        ShowPanels();
        KnapsackAddItem();
    }

    private void ShowPanels()// 按下I、E、W、M键，往背包添加一个随机物品
    {
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
    private void KnapsackAddItem()// 按下K、C、G键，显示/隐藏背包、箱子、装备
    {
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
        else if(Input.GetKeyDown(KeyCode.V))
        {
            inventoryMgr.vendorPanel.SwitchDisplayState();
        }
    }
    private void UpdateCoin()
    {
        coinText.text = coinAmount.ToString();
    }
    public bool ConsumeCoin(int amount)
    {
        if(amount>coinAmount||amount<0)return false;
        coinAmount -= amount;
        UpdateCoin();
        return true;
    }
    public void EarnCoin(int amount)
    {
        coinAmount += amount;
        UpdateCoin();
    }
    public void AddBasicProperty(ref int strength, ref int intellect, ref int agility, ref int stamina, ref int damage)
    {
        strength += basicProperty.strength;
        intellect += basicProperty.intellect;
        agility += basicProperty.agility;
        stamina += basicProperty.stamina;
        damage += basicProperty.damage;
    }
}
