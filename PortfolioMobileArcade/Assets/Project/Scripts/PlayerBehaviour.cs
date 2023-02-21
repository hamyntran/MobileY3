using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

/*------------------------------------------
Author: NAME
Last modified by: NAME
-------------------------------------------*/
 
/// <summary>
/// 
/// </summary>

public enum PlayerAnimation
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
    [SerializeField] private GameObject player;
    private Animator _playerAnim;

    #endregion
    
    #region Properties
    public Animator PlayerAnim
    {
        get => _playerAnim;
    }
    #endregion
    
    #region Constructor
    public PlayerBehaviour()
    {
    
    }
    #endregion
    
    #region Unity Callbacks
    private void Start()
    {
        _playerAnim = player.GetComponent<Animator>();
    }
    	
    private void Update()
    {
    
    }
    #endregion
    
    #region Methods

    public void TriggerPlayerAnimation(PlayerAnimation animation)
    {
        switch (animation)
        {
            case(PlayerAnimation.PlayerAttack):
                _playerAnim.SetTrigger("Attack");
                break;
            case(PlayerAnimation.PlayerJump):
                _playerAnim.SetTrigger("Jump");
                break;
        }
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