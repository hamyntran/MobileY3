using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] TextMeshProUGUI priceTMP;

    private ShopItemData _itemData;
    private ITEMSTATUS _itemstatus;

    [SerializeField] private GameObject buyButton, useButton, usingText;

    public enum ITEMSTATUS
    {
        New = 0,
        Bought = 1,
        Using = 2,
        Unknown = 3
    }

    /*
    private void OnEnable()
    {
        SetStatusButton(GetCurrentStatus());
    }
    */

    private void Start()
    {
        GetCurrentStatus(true);
    }

    private void GetCurrentStatus(bool saveUsing = false)
    {
        if (_itemData != null)
        {
            switch (PlayerPrefs.GetString($"{_itemData.ItemName}/{_itemData.Price}"))
            {
                case Const.Item_New:
                    buyButton.SetActive(true);
                    useButton.SetActive(false);
                    usingText.SetActive(false);
                    _itemstatus = ITEMSTATUS.New;
                    break;

                case Const.Item_Bought:
                    buyButton.SetActive(false);
                    useButton.SetActive(true);
                    usingText.SetActive(false);
                    _itemstatus = ITEMSTATUS.Bought;
                    break;


                case Const.Item_Using:
                    buyButton.SetActive(false);
                    useButton.SetActive(false);
                    usingText.SetActive(true);
                    _itemstatus = ITEMSTATUS.Using;

                    if (saveUsing)
                    {
                        if (!ShopSaveData.previousItemsSlot.ContainsKey(_itemData.Category))
                        {
                            ShopSaveData.previousItemsSlot.Add(_itemData.Category, this);
                        }
                        else
                        {
                            ShopSaveData.previousItemsSlot[_itemData.Category] = this;
                        }
                    }

                    break;

                default:
                    buyButton.SetActive(false);
                    useButton.SetActive(false);
                    usingText.SetActive(false);
                    _itemstatus = ITEMSTATUS.Unknown;
                    break;
            }
        }
    }

    public void Init(ShopItemData itemData)
    {
        _itemData = itemData;
        nameTMP.text = itemData.ItemName;
        priceTMP.text = itemData.Price.ToString();
    }

    public void PurchasedItem()
    {
        if (CoinManager.Instance.CheckCoin(_itemData.Price))
        {
            Shop.OnItemBought?.Invoke(_itemData);

            if (ShopSaveData.previousItemsSlot.ContainsKey(_itemData.Category))
            {
                ShopSaveData.previousItemsSlot[_itemData.Category].GetCurrentStatus();
            }

            GetCurrentStatus(true);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void UseItem()
    {
        Shop.OnItemUse?.Invoke(_itemData);

        if (ShopSaveData.previousItemsSlot.ContainsKey(_itemData.Category))
        {
            ShopSaveData.previousItemsSlot[_itemData.Category].GetCurrentStatus();
        }

        GetCurrentStatus(true);
    }
}