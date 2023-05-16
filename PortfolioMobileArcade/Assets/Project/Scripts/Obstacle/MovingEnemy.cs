using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingEnemy : MonoBehaviour
{
    [SerializeField] float speed = 4;
    private Animator _animator;

    private bool stunning = false;
    // Start is called before the first frame update
    void Start()
    {
        speed += Random.Range(0, 0.5f);
        _animator = GetComponent<Animator>();
        _animator.SetInteger("Walk",1);

        Actions.Stun += Stun;
        Actions.FinishedStunning += Continue;
    }

    private void OnDisable()
    {
        Actions.Stun -= Stun;
        Actions.FinishedStunning -= Continue;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(stunning) {return;}
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Stun()
    {
        stunning = true;
        _animator.SetInteger("Walk",0);
    }

    private void Continue()
    {
        stunning = false;
        _animator.SetInteger("Walk",1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             Actions.HitPlayer.Invoke();
        }
    }
}
