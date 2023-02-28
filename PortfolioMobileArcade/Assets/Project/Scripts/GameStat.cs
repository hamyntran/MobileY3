using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    private int _destroyedObstacle =0;
    private int _gainedCoin =0;
    
    // Start is called before the first frame update
    void Start()
    {
        Actions.DestroyObstacle += UpdateDestroyedObstacles;
        Actions.GainCoin += UpdateGainedCoin;
    }

    private void UpdateDestroyedObstacles(IDestroyable go)
    {
        _destroyedObstacle++;
        
        InGameManager.Instance.GameUI.SetDestroyText(_destroyedObstacle);
    }
    
    private void UpdateGainedCoin(int coin)
    {
        _gainedCoin+= coin;
        InGameManager.Instance.GameUI.SetCoinText(_gainedCoin);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
