using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCoin : MonoBehaviour
{
   public static Action<int> OnGainedCoin;
   private int _coinGained;
   [SerializeField] private InGameCoinUI coinUI;
   private void OnEnable()
   {
      OnGainedCoin += UpdateCoinGained;
   }

   private void OnDisable()
   {
      OnGainedCoin -= UpdateCoinGained;
   }

   public void UpdateCoinGained(int amount)
   {
      _coinGained += amount;
      coinUI.SetCoinText(_coinGained);
   }
   
   
}
