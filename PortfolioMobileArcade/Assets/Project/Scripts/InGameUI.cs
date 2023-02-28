using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI destroyText;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        SetCoinText(0);
        SetDestroyText(0);
    }

    public void SetDestroyText(int quant)
    {
        destroyText.text = $"Destroyed: {quant}";
    }

    public void SetCoinText(int quant)
    {
        coinText.text = $"Coin: {quant}";
    }
}
