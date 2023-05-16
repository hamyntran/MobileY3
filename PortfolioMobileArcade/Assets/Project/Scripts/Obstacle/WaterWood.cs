using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWood : MonoBehaviour
{
    [SerializeField] float speed = 0.3f;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _animator.SetTrigger("Shake");
    }
    
    public void DoneShaking(){}
}
