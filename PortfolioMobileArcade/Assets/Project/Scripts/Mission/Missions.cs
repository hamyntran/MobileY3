using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missions : MonoBehaviour
{
   private List<MissionTypeData> allMissions = new List<MissionTypeData>();
   [SerializeField] private MissionTile missionTile;
   [SerializeField] private Transform missionContent;

   private void Awake()
   {
      allMissions = DataManager.Instance.MissionData.MissionTypeDatas;

      foreach (MissionTypeData mission in allMissions)
      {
        var newTile =  Instantiate(missionTile, missionContent);
        newTile.Init(mission);
      }
   }

}
