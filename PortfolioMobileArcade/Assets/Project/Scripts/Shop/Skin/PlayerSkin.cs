using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public string SkinName;
    public ShopItemData Data;

    public void Init()
    {
        Data = GetItemData();
    }

    private ShopItemData GetItemData()
    {
        return DataManager.Instance.ShopData.GetShopItem(SkinName);
    }

    public bool CheckInUse()
    {
        if (Data == null)
        {
            return false;}
        return PlayerPrefs.GetString($"{Data.ItemName}/{Data.Price}") == Const.Item_Using;
    }
}
