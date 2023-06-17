using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTMP;

    public void SetCoinText(int amount)
    {
        coinTMP.text = amount.ToString();
    }
}
