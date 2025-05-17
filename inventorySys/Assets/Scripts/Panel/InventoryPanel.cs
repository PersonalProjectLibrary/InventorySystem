
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 背包基类
/// </summary>
public class InventoryPanel : MonoBehaviour
{
    public Player player{ get; private set; }
    protected InventoryManager inventoryMgr;
    protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            return canvasGroup;
        }
    }

    protected int slotCount = 0;
    public Transform slotParent;
    public GameObject slotPrefab;
    protected List<Slot> slotList=new List<Slot>();
    
    private float targetAlpha;
    
    #region Init Panel
    public void Start()
    {
        Init();
        InstanticeSlot();
        Hide();
    }
    protected virtual void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inventoryMgr = InventoryManager.Instance;
        canvasGroup = GetComponent<CanvasGroup>();
        //清除slotParent下面的子物体
        for (int i = slotParent.childCount - 1; i >= 0; i--)
        {
            Destroy(slotParent.GetChild(i).gameObject);
        }
    }
    protected virtual void InstanticeSlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGo = Instantiate(slotPrefab, slotParent);
            slotList.Add(slotGo.GetComponent<Slot>());
        }
    }
    #endregion

    #region Panel Control
    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Constant.AlphaSmoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
    protected void Show()
    {
        targetAlpha = Constant.ShowAlpha;
        canvasGroup.blocksRaycasts = true;
    }
    protected void Hide()
    {
        targetAlpha = Constant.HideAlpha;
        canvasGroup.blocksRaycasts = false;//不接收射线，射线会穿透界面，更不会触发射线检测鼠标点击事件
    }
    public void SwitchDisplayState()
    {
        if (canvasGroup.alpha == Constant.ShowAlpha)Hide();
        else Show();
    }
    #endregion

    #region Item And  Slot
    public bool ObtainItem(int itemId)
    {
        ItemData itemData = inventoryMgr.GetItemDataById(itemId);
        return ObtainItem(itemData);
    }
    public bool ObtainItem(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.LogError("要获取的物品不存在");
            return false;
        }
        Slot slot;
        if(itemData.Capacity == 1)// 物品容量为1，不能和其他同类物品放一起
        {
            slot = FindEmptyItemSlot();
            if (slot == null)
            {
                Debug.Log("没有空的物品槽");
                return false;
            }
            slot.StoreItem(itemData);
        }
        else
        {
            slot = FindSameItemSlot(itemData);
            if(slot == null)//没有存储过该物品的格子
            {
                slot = FindEmptyItemSlot();
                if (slot == null)
                {
                    Debug.Log("没有空的物品槽");
                    return false;
                }
                slot.StoreItem(itemData);
            }
            else slot.UpdateItemCount(slot.item.ItemAmount + 1);
        }
        //Debug.Log(itemData.Name + "获取成功");
        return true;
    }
    public Slot FindEmptyItemSlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.IsEmpty())return slot;
        }
        return null;
    }
    public Slot FindSameItemSlot(ItemData itemData)
    {
        foreach (Slot slot in slotList)
        {
            // 如果格子里有物品，并且物品ID相同，并且物品数量没有达到上限
            if (slot.item != null && slot.item.selfData.ID == itemData.ID && !slot.IsFilled())
            {
                return slot;
            }
        }
        return null;
    }
    #endregion

    #region Save and Load
    public void SaveInventory()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Slot slot in slotList)
        {
            if (slot.IsEmpty())
            {
                sb.Append("-1:0|");
                continue;
            }
            sb.Append(slot.item.selfData.ID + ":" + slot.item.ItemAmount + "|");
        }
        //sb.Remove(sb.Length - 1, 1);//法一：删除最后一个|，避免解析字符串分隔时，数组最后多个空字符串
        PlayerPrefs.SetString(name, sb.ToString());//默认在OnApplicationQuit()时自动保存
        PlayerPrefs.Save();//立即保存，避免以外退出时未触发自动保存，导致数据丢失
        //Debug.Log($"{name}的物品{sb}保存完毕");
    }
    public void LoadInventory()
    {
        if(!PlayerPrefs.HasKey(name))return;
        string[] itemStrings = PlayerPrefs.GetString(name).Split('|');
        //for (int i = 0; i < itemStrings.Length; i++)
        for (int i = 0; i < itemStrings.Length - 1; i++)//法二：不解析按‘|’切割字符串，数组额外多出来的空字符
        {
            string[] itemString = itemStrings[i].Split(':');
            if (itemString[0] == "-1")
            {
                if(!slotList[i].IsEmpty())//避免保存后，往空格子放物品，导致加载旧数据，空格子依旧有物品，未还原
                {
                    slotList[i].ClearItem();
                }
                continue;
            }
            int itemId = int.Parse(itemString[0]);
            int itemCount = int.Parse(itemString[1]);
            ItemData itemData = inventoryMgr.GetItemDataById(itemId);
            slotList[i].StoreItem(itemData, itemCount);
        }
        //Debug.Log($"{name}的物品{PlayerPrefs.GetString(name)}加载完毕");
    }
    #endregion
}
