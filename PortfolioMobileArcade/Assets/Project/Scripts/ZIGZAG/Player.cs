using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;

    private Vector3 _moveDirection = new Vector3();
    [SerializeField] private LayerMask playerMask;

    private void Start()
    {
        _moveDirection = Vector3.zero; 
        InvokeRepeating(nameof(CheckFailling),1,3);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_moveDirection == Vector3.forward)
            {
                _moveDirection = Vector3.left;
            }
            else
            {
                _moveDirection = Vector3.forward;
            }
        }
        
       transform.Translate(_moveDirection * movementSpeed * Time.deltaTime);
    }

    private void CheckFailling()
    {
        if(Physics.Raycast(transform.position,-Vector3.up,out RaycastHit  hit,playerMask))
        {
            if (hit.distance > 100)
            {
                
            }
        }
    }
}
