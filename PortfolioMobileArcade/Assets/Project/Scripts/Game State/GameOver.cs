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
      AdsManager.Instance.PlayRewardAd(
         () =>
         {
            AddGainedCoin(true);
            OutGame();
         });
   }

   private void AddGainedCoin(bool doubleReward =false)
   {
      if (!doubleReward)
      {
         CoinManager.GainCoin?.Invoke(InGame.CoinGained);
      }
      else
      {

//         Debug.Log($"Gained {InGame.CoinGained * 2}");

         CoinManager.GainCoin?.Invoke(InGame.CoinGained * 2);
      }
   }

   public void OutGame()
   {
      GameManager.OnSwitchState?.Invoke(GameState.Begin);
      ObjectPool.Instance.ClearAllPools();
      SceneManager.LoadScene(0);
   }
}
