using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;

    private Vector3 _moveDirection = new Vector3();

    private void Start()
    {
        _moveDirection = Vector3.zero; 
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
}
