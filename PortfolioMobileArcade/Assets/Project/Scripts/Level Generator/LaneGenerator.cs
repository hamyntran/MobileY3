using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LaneGenerator : MonoBehaviour
{
    public static LaneGenerator Instance;
    
    [SerializeField] private List<LaneDatas> laneDatas;
    [SerializeField] private int laneWidth = 1;

    public int LaneWidth
    {
        get => laneWidth;
    }

    [SerializeField] private int maxLanes = 10;

    private List<GameObject> currentLanes = new List<GameObject>() ;
    private Vector3 currentPos;
    
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(this);
    }

    
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
