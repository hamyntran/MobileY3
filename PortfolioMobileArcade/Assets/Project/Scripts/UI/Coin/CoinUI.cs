using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTMP;

    private void OnEnable()
    {
        SetCoinText(CoinManager.Coin);
        CoinManager.GainCoin +=(c)=> SetCoinText(CoinManager.Coin);
    }

    private void OnDisable()
    {
        CoinManager.GainCoin -=(c)=> SetCoinText(CoinManager.Coin);
    }

    public void SetCoinText(int amount)
    {
        coinTMP.text = amount.ToString();
    }
}
