using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionTile : MonoBehaviour
{
    private MissionTypeData _data;
    [SerializeField] private TextMeshProUGUI missionText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private GameObject claimButton, rewardGO;
    [SerializeField] private TextMeshProUGUI progressTMP;
    
    public void Init(MissionTypeData data)
    {
        _data = data;
    }
}