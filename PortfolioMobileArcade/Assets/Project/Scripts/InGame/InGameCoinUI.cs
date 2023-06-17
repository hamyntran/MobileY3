using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameCoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTMP;

    private void Start()
    {
        SetCoinText(0);
    }

    public void SetCoinText(int amount)
    {
        coinTMP.text = amount.ToString();
    }
}
