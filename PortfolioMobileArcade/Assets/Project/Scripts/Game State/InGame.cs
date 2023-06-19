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
    }

    private void OnEnable()
    {
        OnGainCoinInGame += AddCoinAmount;
        GameManager.OnPlayerDied += () => {  };
    }

    private void EnableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }


    private void OnDisable()
    {
        GameManager.OnPlayerDied -= () => { gameOverPanel.SetActive(true); };
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
