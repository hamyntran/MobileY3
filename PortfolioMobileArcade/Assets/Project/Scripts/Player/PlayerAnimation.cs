using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnim;
    public bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponent<Animator>();

    }

    private void Update()
    {
        attacking = _playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack04_SwordAndShiled");
    }

    public void TriggerPlayerAnimation(PlayerAnimationState animation)
    {
        switch (animation)
        {
            case(PlayerAnimationState.PlayerAttack):
                _playerAnim.SetTrigger("Attack");
                break;
            case(PlayerAnimationState.PlayerJump):
                _playerAnim.SetTrigger("Jump");
                break;
        }
    }

}
