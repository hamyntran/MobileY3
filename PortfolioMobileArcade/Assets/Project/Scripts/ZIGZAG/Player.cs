using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;

    private Vector3 _moveDirection = new Vector3();
    [SerializeField] private LayerMask playerMask;

    private bool _die = false;

    private void Start()
    {
        _die = false;
    }

    private void OnEnable()
    {
        GameManager.OnSwitchState += StartMoving;
    }

    private void OnDisable()
    {
        GameManager.OnSwitchState -= StartMoving;
    }

    private void StartMoving(GameState state)
    {
        if (state == GameState.InGame) 
        {
            _moveDirection = Vector3.forward;
        }
    }

    private void Update()
    {
        if(_die) {return;}
        if (GameManager.Instance.CurrentState != GameState.InGame)
        {
            return;
        }

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

        CheckFailling();
    }

    private void CheckFailling()
    {
        if (!Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, playerMask))
        {

            StartCoroutine(WaitToEndGame());
        }
    }

    private IEnumerator WaitToEndGame()
    {
        yield return new WaitForSeconds(1f);
        _die = true;

        GameManager.OnPlayerDied?.Invoke();
    }
}