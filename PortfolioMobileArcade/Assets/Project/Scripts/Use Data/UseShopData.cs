using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UseShopData : MonoBehaviour
{
    [SerializeField] private ShopItemSlot itemSlot;
    [SerializeField] private Transform characterSlotContainer, bgSlotContainer;

    private ShopData _shopData;

    private void Awake()
    {
        _shopData = DataManager.Instance.ShopData;

        for (int i = 0; i < _shopData.ShopItemDatas.Count; i++)
        {
            ShopItemSlot newSlot = itemSlot.SpawnFromPool();

            switch (_shopData.ShopItemDatas[i].Category)
            {
                case CATEGORY.Character:
                    newSlot.transform.SetParent(characterSlotContainer, false);
                    break;
                
                case CATEGORY.Background:
                    newSlot.transform.SetParent(bgSlotContainer, false);
                    break;
            }
            
            newSlot.SetSlotUI(_shopData.ShopItemDatas[i].ItemName,_shopData.ShopItemDatas[i].Price);
        }
    }
}