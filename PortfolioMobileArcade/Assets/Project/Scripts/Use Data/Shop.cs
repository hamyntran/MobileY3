using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour
{
    public static Action<ShopItemData> OnItemBought;
    public static Action<ShopItemData> OnItemUse;

    [SerializeField] private ShopItemSlot itemSlot;
    [SerializeField] private Transform characterSlotContainer, bgSlotContainer;
    [SerializeField] private GameObject characterShopList, bgShopList;

    [SerializeField] private TextMeshProUGUI categoryTitleTMP;

    private Action<CATEGORY> OnChangeCategory;
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

            newSlot.Init(_shopData.ShopItemDatas[i]);
            
        }
    }

    public void ChangeCategory(int enumID)
    {
        if (Enum.IsDefined(typeof(CATEGORY), enumID))
        {
            OnChangeCategory?.Invoke((CATEGORY)enumID);
        }
    }

    private void Start()
    {
        OnChangeCategory += ChangeCategoryTitleText;
        OnChangeCategory += SwitchShopListPanel;
        
    }

    private void OnEnable()
    {
        OnChangeCategory?.Invoke(CATEGORY.Character);
    }

    private void OnDisable()
    {
        OnChangeCategory -= ChangeCategoryTitleText;
        OnChangeCategory -= SwitchShopListPanel;
    }

    private void ChangeCategoryTitleText(CATEGORY category) => categoryTitleTMP.text = category.ToString();

    private void SwitchShopListPanel(CATEGORY category)
    {
        switch (category)
        {
            case CATEGORY.Character:
                characterShopList.SetActive(true);
                bgShopList.SetActive(false);
                return;
            case CATEGORY.Background:
                characterShopList.SetActive(false);
                bgShopList.SetActive(true);
                return;
        }
    }
}