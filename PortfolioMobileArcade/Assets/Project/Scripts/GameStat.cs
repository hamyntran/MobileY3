using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    private int _destroyedObstacle =0;
    
    // Start is called before the first frame update
    void Start()
    {
        Actions.DestroyObstacle += UpdateDestroyedObstacles;
    }

    private void UpdateDestroyedObstacles(ISwordAttacked go)
    {
        _destroyedObstacle++;
        
        InGameManager.Instance.GameUI.SetDestroyText(_destroyedObstacle);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
