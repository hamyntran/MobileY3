using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;


public class ShopSaveData : MonoBehaviour
{
    private string CreatedItemStatus
    {
        get => PlayerPrefs.GetString("CreatedItemStatus", "false");
        set
        {
            if (value == "false" || value == "False")
            {
                PlayerPrefs.SetString("CreatedItemStatus", "false");
            }
            else if(value == "true" || value == "True")
            {
                PlayerPrefs.SetString("CreatedItemStatus", "true");
            }
        }
    }

    public string UsingCharacter
    {
        get => PlayerPrefs.GetString("UsingCharacter", "");
        set { PlayerPrefs.SetString("UsingCharacter", value); }
    }

    private string UsingBG
    {
        get => PlayerPrefs.GetString("UsingBG", "");
        set { PlayerPrefs.SetString("UsingBG", value); }
    }

    public static Dictionary<CATEGORY, ShopItemSlot> previousItemsSlot = new Dictionary<CATEGORY, ShopItemSlot>();

    //Debug Purpose
    [SerializeField] private List<ItemStatus> ItemStatusList = new List<ItemStatus>();

    private void Start()
    {
        if (CreatedItemStatus == "false")
        {
            ResetShoppingHistory();
        }

        Shop.OnItemBought += UpdateStatus;
        Shop.OnItemUse += UpdateStatus;
    }

    private void OnDestroy()
    {
        Shop.OnItemBought -= UpdateStatus;
        Shop.OnItemUse -= UpdateStatus;
    }


    List<ShopItemData> allItems => DataManager.Instance.ShopData.ShopItemDatas;

    public void ResetShoppingHistory()
    {
        ItemStatusList.Clear();

        UsingBG = "";
        UsingCharacter = "";

        foreach (var item in allItems)
        {
            if (item.Price != 0)
            {
                PlayerPrefs.SetString($"{item.ItemName}/{item.Price}", "new");
#if UNITY_EDITOR
                ItemStatusList.Add(new ItemStatus(item, false));
#endif //Set PlayerPref
            }
            else
            {
                UpdateStatus(item);

#if UNITY_EDITOR
                ItemStatusList.Add(new ItemStatus(item, true));
#endif
            }
        }

        CreatedItemStatus = "true";
        /*Debug.Log(
            $"Created item data status. Created {allItems.Count}/{DataManager.Instance.ShopData.ShopItemDatas.Count}");*/
    }

    public void UpdateDebug()
    {
#if UNITY_EDITOR
        foreach (var item in ItemStatusList)
        {
            item.GetStatus(PlayerPrefs.GetString($"{item.ItemData.ItemName}/{item.ItemData.Price}", "?"));
        }
#endif
    }

    public void UpdateStatus(ShopItemData itemData)
    {
        if (itemData.Category == CATEGORY.Character)
        {
            if (UsingCharacter != "")
            {
                PlayerPrefs.SetString(UsingCharacter, "bought"); //Update previous using Item
            }

            UsingCharacter = $"{itemData.ItemName}/{itemData.Price}"; //Update _usingCharacter
            PlayerPrefs.SetString(UsingCharacter, "using"); //Set PlayerPref
        }
        else if (itemData.Category == CATEGORY.Background)
        {
            if (UsingBG != "")
            {
                PlayerPrefs.SetString(UsingBG, "bought");
            }

            UsingBG = $"{itemData.ItemName}/{itemData.Price}";
            PlayerPrefs.SetString(UsingBG, "using");
        }

        //UpdateDebug();
    }
    
    
}


[System.Serializable]
public class ItemStatus
{
    public ShopItemData ItemData;
    public string Name;
    public string Bought;

    public ItemStatus(ShopItemData data, bool bought)
    {
        ItemData = data;
        Name = data.ItemName;
        Bought = bought.ToString();
    }

    public void GetStatus(string status)
    {
        Bought = status;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(ShopSaveData))]
public class CoinShopSaveData : Editor
{
    public override void OnInspectorGUI()
    {
        ShopSaveData _target = (ShopSaveData)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Reset Shopping History"))
        {
            _target.ResetShoppingHistory();
        }

        if (GUILayout.Button("Update Debug"))
        {
            _target.UpdateDebug();
        }
    }
}
#endif