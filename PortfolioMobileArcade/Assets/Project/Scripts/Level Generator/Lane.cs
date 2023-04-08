
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

    private int _tensionGapMin => InGameManager.Instance.Generator.MinTension ;
    private int _tensionGapMax => InGameManager.Instance.Generator.MaxTension;


    private int _gapScale => InGameManager.Instance.Generator.LaneWidth;


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
        pos.x -= (_width / 2);
        pos.y += (_height / 2);
        _spawnPos = pos;

        Spawn();
    }

    private void Update()
    {
    }

    #endregion

    #region Methods

    private void Spawn()
    {
        while (_totalGap < _width - _tensionGapMax *_gapScale)
        {
            GameObject obstacle = _obstacles.GetRandom();
            int gap = UnityEngine.Random.Range(_tensionGapMin*_gapScale, _tensionGapMax *_gapScale);

            _totalGap += gap;

            UpdateSpawnPosition(gap);

            GameObject newLane = Instantiate(obstacle, _spawnPos, Quaternion.identity);
            newLane.transform.parent = transform;
        }
    }

    private void UpdateSpawnPosition(float gap)
    {
        var posX = _spawnPos.x;

        posX += gap;

        _spawnPos = new Vector3(posX, _spawnPos.y, _spawnPos.z);
    }

    #endregion
}