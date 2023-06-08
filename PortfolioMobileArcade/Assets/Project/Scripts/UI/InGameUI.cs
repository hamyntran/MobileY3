using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI destroyText;

    [SerializeField] private TextMeshProUGUI coinTMP;

    private void Start()
    {
        Actions.GainCoin += SetCoinText;
        Shop.OnItemBought += SetCoinText;
        SetDestroyText(0);
        SetCoinText();
    }

    private void OnDestroy()
    {
        Actions.GainCoin -= SetCoinText;
        Shop.OnItemBought -= SetCoinText;
    }

    public void SetDestroyText(int quant)
    {
        destroyText.text = $"Destroyed: {quant}";
    }

    public void SetCoinText(int addition)
    {
        coinTMP.text = CoinManager.Instance.Coin.ToString();
    }
    
    public void SetCoinText(ShopItemData addition)
    {
        coinTMP.text = CoinManager.Instance.Coin.ToString();
    }
    
    public void SetCoinText()
    {
        coinTMP.text = CoinManager.Instance.Coin.ToString();
    }
}
