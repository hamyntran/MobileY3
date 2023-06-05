using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField] private WeightedRandomList<Obstacle> _obstacles;

    [SerializeField] private int _tensionGapMin = 1;
    [SerializeField] private int _tensionGapMax = 3;

    private int _gapScale => InGameManager.Instance.Generator.LaneWidth;
    private int _trySpawn => InGameManager.Instance.Generator.TrySpawn;
    private Vector3 _spawnPos = new Vector3();

    private float _width;
    private float _height;

    private float _totalGap = 0;

    #endregion

    #region Properties

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        _width = transform.localScale.x;
        _height = transform.localScale.y;

        var pos = gameObject.transform.position;
        pos.x -= (_width / 2) - 1;
        pos.y += (_height / 2);
        _spawnPos = pos;

        InGameManager.Instance.OnGameStart += () => {  if (_obstacles.Count > 0) Spawn();};
    }

    private void OnDisable()
    {
        InGameManager.Instance.OnGameStart -= () => {  if (_obstacles.Count > 0) Spawn();};
    }

    private void Update()
    {
    }

    #endregion

    #region Methods

    private void Spawn()
    {
        while (_totalGap < _width - _tensionGapMax * _gapScale)
        {
            Obstacle obstacle = _obstacles.GetRandom();
            int gap = Random.Range(_tensionGapMin * _gapScale, _tensionGapMax * _gapScale);

            Obstacle newOpstacle = Instantiate(obstacle, _spawnPos, Quaternion.identity);
            newOpstacle.transform.parent = transform;

            _totalGap += (gap + newOpstacle.Length);

            if (_totalGap > _width - _tensionGapMax * _gapScale)
            {
                _totalGap -= (gap + newOpstacle.Length);
                Destroy(newOpstacle.gameObject);
                TrySpawn();
                return;
            }
            
            newOpstacle._ativation?.Activate(newOpstacle);

            UpdateSpawnPosition(gap, newOpstacle.Length);
        }
    }

    protected void TrySpawn()
    {
        for (int t = 0; t < _trySpawn; t++)
        {
            Obstacle obstacle = _obstacles.GetRandom();
            int gap = ((_tensionGapMin * _gapScale) >0)? _tensionGapMin * _gapScale : 1;

            Obstacle newOpstacle = Instantiate(obstacle, _spawnPos, Quaternion.identity);

            newOpstacle.transform.parent = transform;
            _totalGap += (gap + newOpstacle.Length);

            if (_totalGap > _width - _tensionGapMin * _gapScale)
            {
                _totalGap -= (gap + newOpstacle.Length);
                Destroy(newOpstacle.gameObject);
            }
            else
            {
                newOpstacle._ativation?.Activate(newOpstacle);

                UpdateSpawnPosition(gap, newOpstacle.Length);  
            }
        }
    }

    private void UpdateSpawnPosition(float gap, int length)
    {
        var posX = _spawnPos.x;

        posX += (gap + length);

        _spawnPos = new Vector3(posX, _spawnPos.y, _spawnPos.z);
    }

    #endregion
}