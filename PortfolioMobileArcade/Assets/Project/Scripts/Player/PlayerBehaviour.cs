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
    }
    	
    private void Update()
    {
    
    }



    private void OnCollisionEnter(Collision collision)
    {
    }
    

    #endregion
    
    #region Methods

   
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

