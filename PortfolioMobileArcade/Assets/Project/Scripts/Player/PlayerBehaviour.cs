 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
 using Unity.VisualScripting;

 /*------------------------------------------
Author: NAME
Last modified by: NAME
-------------------------------------------*/
 
/// <summary>
/// 
/// </summary>

public enum PlayerAnimationState
{
    PlayerIdle,
    PlayerRun,
    PlayerAttack,
    PlayerJump,
    PlayerSpin,
    PlayerDie
}

public class PlayerBehaviour : MonoBehaviour
 {
    #region Variables

    public PlayerAnimation playerAnimation;
    [SerializeField] private Sword _sword;
    
    
    private UnitHealth _health;
    [SerializeField] private int _maxHealth = 100;

    #endregion
    
    #region Properties
    #endregion
    
    #region Constructor
    public PlayerBehaviour()
    {
    
    }
    #endregion
    
    #region Unity Callbacks
    private void Start()
    {
        _sword.Init(this);
        _health = new UnitHealth(_maxHealth, _maxHealth);

        Actions.HitPlayer += HitByOther;
    }
    	
    private void Update()
    {
    
    }

    #endregion

    #region Methods

    private void HitByOther()
    {
        //Play animation
        Debug.Log("die");
        //Die
        
        //Game Over
    }

    #endregion
    
}

[CustomEditor(typeof(PlayerBehaviour))]
public class PlayerBehaviourEditorCustom: Editor
 {
     public override void OnInspectorGUI()
     {
        PlayerBehaviour _target = (PlayerBehaviour)target;
        DrawDefaultInspector();
     }
}

