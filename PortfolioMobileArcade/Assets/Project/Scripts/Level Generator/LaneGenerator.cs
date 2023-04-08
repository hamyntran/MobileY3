using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class LaneGenerator : MonoBehaviour
{
    [SerializeField] private List<LaneDatas> laneDatas;
    [SerializeField] private int laneWidth = 1;

    public int MinTension;
    public int MaxTension;
    public int MinPortalRange;
    public int MaxPortalRange;
    public int LaneWidth => laneWidth;

    [SerializeField] private int maxLanes = 10;

    private List<GameObject> currentLanes = new List<GameObject>();
    private Vector3 currentPos;


    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;

        for (int i = 0; i < maxLanes; i++)
        {
            GenerateLane(true);
        }

        maxLanes = currentLanes.Count;
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
            // newLane.transform.parent = transform;
        }

        if (!start)
        {
            if (currentLanes.Count > maxLanes)
            {
                Destroy(currentLanes[0]);
                currentLanes.RemoveAt(0);
            }
        }
    }
}