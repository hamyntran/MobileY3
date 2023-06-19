using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI coinTMP;
   
   public void SetCoinText()
   {
      coinTMP.text = $"x{InGame.CoinGained.ToString()}";
   }
   public void Claim()
   {
      AddGainedCoin();
      OutGame();
   }

   public void DoubleClaim()
   {
      
   }

   private void AddGainedCoin()
   {
      CoinManager.GainCoin?.Invoke(InGame.CoinGained);
   }

   private void OutGame()
   {
      GameManager.OnSwitchState?.Invoke(GameState.Begin);

      SceneManager.LoadScene(0);

      ObjectPool.Instance.ClearAllPools();
   }
}
