using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

public class LaneGenerator : MonoBehaviour
{
    [SerializeField] private List<LaneDatas> laneDatas;
    private Vector3 currentPos;
    [SerializeField] private int laneWidth = 1;

    [SerializeField] private int maxLanes = 10;

    private List<GameObject> currentLanes = new List<GameObject>() ;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        
        for (int i = 0; i < maxLanes; i++)
        {
            GenerateLane();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GenerateLane();
        }
    }

    /// <summary>
    /// Later Inprovement: Object Pooling
    /// </summary>
    private void GenerateLane()
    {
        int randLane = UnityEngine.Random.Range(0, laneDatas.Count);
        int randQuant = UnityEngine.Random.Range(0, laneDatas[randLane].maxInChain);

        for (int i = 0; i < randQuant; i++)
        {
            GameObject newLane = Instantiate(laneDatas[randLane].prefab, currentPos, quaternion.identity);
            currentLanes.Add(newLane);
            currentPos.x += laneWidth;
            
            if (currentLanes.Count > maxLanes)
            {
                Destroy(currentLanes[0]);
                currentLanes.RemoveAt(0);
            }
            print(currentLanes.Count);

        }
    }
}
