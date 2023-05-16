using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/*------------------------------------------
Author: NAME
Last modified by: NAME
-------------------------------------------*/
 
/// <summary>
/// 
/// </summary>

public class Obstacle : MonoBehaviour
 {
    #region Variables

    public ISwordAttacked swordAttacked;
    [SerializeField] protected int _length =0;
    public ObstacleAtivation _ativation;
    public int Length => _length;

    #endregion
    
    #region Properties
    
    #endregion
    
    
    #region Unity Callbacks
    protected virtual void Start()
    {
    
    }
    	
    private void Update()
    {
    
    }
    #endregion
    
    #region Methods
    
    #endregion
}

public abstract class ObstacleAtivation
{
	public abstract void Activate(Obstacle obstacle);
}


