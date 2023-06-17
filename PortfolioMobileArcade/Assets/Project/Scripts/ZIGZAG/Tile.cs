using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TileGenerator.Instance.SpawnTile();
            GroundPassed();
        }
    }

    private Vector3 originalLocalPos = new Vector3();

    private void OnEnable()
    {
        originalLocalPos = transform.localPosition;
    }

    private void OnDisable()
    {
        transform.localPosition = originalLocalPos;
    }

    private void GroundPassed()
    {
        var tilePos = transform.position;
       
        transform.DOMoveY(tilePos.y - 10, 1.5f).OnComplete(transform.parent.gameObject.ReturnToPool);
    }
}
