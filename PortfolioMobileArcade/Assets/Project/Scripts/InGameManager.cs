using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonMonoBehaviour<InGameManager>
{
    [SerializeField] private LaneGenerator _laneGenerator;
    [SerializeField] private GameStat _gameStat;
    [SerializeField] private InGameUI _inGameUI;

    public LaneGenerator Generator => _laneGenerator;
    public GameStat Stat => _gameStat;

    public InGameUI GameUI => _inGameUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
