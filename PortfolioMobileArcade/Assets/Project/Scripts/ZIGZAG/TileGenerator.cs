using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : SingletonMonoBehaviour<TileGenerator>
{
    [SerializeField] private GameObject currentTile;
    [SerializeField] private int tileAmount = 15;

    [SerializeField] private List<GameObject> tiles;
    
    void Start()
    {
        for (int i = 0; i < tileAmount; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        int random = Random.Range(0, 2);
        currentTile = tiles[random].SpawnFromPool(currentTile.transform.GetChild(0).transform.GetChild(random).position);
    }
}
