
using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform coinPos;
    [SerializeField] private GameObject coinPrefab;
    private GameObject spawnedItem;
    
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
        
        var random = Random.Range(0, TileGenerator.Instance.CoinPosibility);
        if (random == 0)
        {
            spawnedItem = coinPrefab.SpawnFromPool(coinPos.position, quaternion.identity);
            spawnedItem.transform.SetParent(transform);
        }
    }

    private void Start()
    {
       
    }

    private void OnDisable()
    {
        transform.localPosition = originalLocalPos;
        
        if (spawnedItem != null && spawnedItem.transform.IsChildOf(transform) && spawnedItem.activeSelf)
        {
            spawnedItem.ReturnToPool();
            spawnedItem = null;
        }
    }

    private void GroundPassed()
    {
        var tilePos = transform.position;
       
        transform.DOMoveY(tilePos.y - 10, 1.5f).OnComplete(transform.parent.gameObject.ReturnToPool);
    }
}
