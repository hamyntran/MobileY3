using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
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
      GameManager.OnSwitchState?.Invoke(GameManager.GameState.Begin);

      SceneManager.LoadScene(0);

      ObjectPool.Instance.ClearAllPools();
   }
}
