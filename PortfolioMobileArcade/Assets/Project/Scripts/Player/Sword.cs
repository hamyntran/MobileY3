using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool _attacking => _playerBehaviour.playerAnimation.attacking;
    private PlayerBehaviour _playerBehaviour;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(PlayerBehaviour playerBehaviour)
    {
        _playerBehaviour = playerBehaviour;
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle destroyale = other.GetComponent<Obstacle>();
        if (  destroyale != null && _attacking)
        {
            destroyale.Destroyable?.Destroyed(other.gameObject);
        }
    }
}
