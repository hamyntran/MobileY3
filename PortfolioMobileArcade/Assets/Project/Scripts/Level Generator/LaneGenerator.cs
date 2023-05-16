using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class LaneGenerator : MonoBehaviour
{
    [SerializeField] private List<LaneDatas> laneDatas;
    [SerializeField] private LaneDatas startLane;

    [SerializeField] private int laneWidth = 1;

    public int TrySpawn = 5;
    public int MinTension;
    public int MaxTension;
    public int MinPortalRange;
    public int MaxPortalRange;
    public int LaneWidth => laneWidth;

    [FormerlySerializedAs("maxLanes")] [SerializeField] private int _maxLaneQuant = 10;
    [FormerlySerializedAs("_startLanes")] [SerializeField] private int _startLaneQuant = 7;

    private List<GameObject> currentLanes = new List<GameObject>();
    private Vector3 currentPos;


    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        
        GenerateLane(true, startLane, _startLaneQuant);


        for (int i = 0; i < _maxLaneQuant; i++)
        {
            GenerateLane(true);
        }

        _maxLaneQuant = currentLanes.Count;
    }

    /// <summary>
    /// Later Inprovement: Object Pooling
    /// </summary>
    public void GenerateLane(bool start)
    {
        int randLane = UnityEngine.Random.Range(0, laneDatas.Count);
        int randQuant = UnityEngine.Random.Range(1, laneDatas[randLane].maxInChain);

        for (int i = 0; i < randQuant; i++)
        {
            GameObject newLane = Instantiate(laneDatas[randLane].prefab, currentPos, quaternion.identity);
            currentLanes.Add(newLane);
            currentPos.z += laneWidth;
             newLane.transform.parent = transform;
        }

        if (!start)
        {
            if (currentLanes.Count > _maxLaneQuant)
            {
                Destroy(currentLanes[0]);
                currentLanes.RemoveAt(0);
            }
        }
    }
    
    
    public void GenerateLane(bool start, LaneDatas laneData, int quant =1)
    {
        for (int i = 0; i < quant; i++)
        {
            GameObject newLane = Instantiate(laneData.prefab, currentPos, quaternion.identity);
            currentLanes.Add(newLane);
            currentPos.z += laneWidth;
             newLane.transform.parent = transform;
        }

        if (!start)
        {
            if (currentLanes.Count > _maxLaneQuant)
            {
                Destroy(currentLanes[0]);
                currentLanes.RemoveAt(0);
            }
        }
    }

}