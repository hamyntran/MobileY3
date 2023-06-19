using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTMP;
    public static int CoinGained = 0;
    public static Action<int> OnGainCoinInGame;
    [SerializeField] private GameObject gameOverPanel;


    private void Start()
    {
        CoinGained = 0;
        SetCoinText();
        
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        OnGainCoinInGame += AddCoinAmount;
        GameManager.OnPlayerDied +=EnableGameOverPanel;
    }

    private void EnableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }


    private void OnDisable()
    {
        GameManager.OnPlayerDied -=EnableGameOverPanel;
        OnGainCoinInGame -= AddCoinAmount;
    }

    private void AddCoinAmount(int add)
    {
        CoinGained += add;

        SetCoinText();
    }

    private void SetCoinText()
    {
        coinTMP.text = CoinGained.ToString();
    }
}
