using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*------------------------------------------
Author: NAME
Last modified by: NAME
-------------------------------------------*/

/// <summary>
/// 
/// </summary>
public class Lane : MonoBehaviour
{
    #region Variables

    [SerializeField] private WeightedRandomList<GameObject> _obstacles;
    [SerializeField] private int _tensionGapMin = 2;
    [SerializeField] private int _tensionGapMax = 4;

    private Vector3 _spawnPos = new Vector3();

    private float width;
    private float height;
    #endregion

    #region Properties

    #endregion

    #region Constructor

    public Lane()
    {
    }

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        width = transform.localScale.x / 2;
        height = transform.localScale.y / 2;
        
        var pos = gameObject.transform.position;
        pos.x -= width;
        pos.y += height;
        _spawnPos = pos;
        Debug.Log(_spawnPos);
    }

    private void Update()
    {
    }

    #endregion

    #region Methods

    private void Spawn()
    {
        GameObject obstacle = _obstacles.GetRandom();
    }
    #endregion
}