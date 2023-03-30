using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float speed = 4;

    private Animator _animator;

    private bool stunning = false;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetInteger("Walk",1);

        Actions.Stun += Stun;
        Actions.FinishedStunning += Continue;
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
}
