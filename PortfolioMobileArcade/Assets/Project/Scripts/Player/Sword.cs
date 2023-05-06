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
        transform.localPosition = new Vector3(0,0,0);
    }

    public void Init(PlayerBehaviour playerBehaviour)
    {
        _playerBehaviour = playerBehaviour;
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (  obstacle != null && _attacking)
        {
            obstacle.swordAttacked?.Attacked(other.gameObject);
        }
    }
}
