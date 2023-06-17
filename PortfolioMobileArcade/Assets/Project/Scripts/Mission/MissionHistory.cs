using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHistory : MonoBehaviour
{
    public static int GamePlayed
    {
        get => PlayerPrefs.GetInt("GamePlayed", 0);
        set { PlayerPrefs.SetInt("GamePlayed", value); }
    }

    public static int ObstacleDestroyed
    {
        get => PlayerPrefs.GetInt("ObstacleDestroyed", 0);
        set { PlayerPrefs.SetInt("ObstacleDestroyed", value); }
    }

    public static int PathPassed
    {
        get => PlayerPrefs.GetInt("PathPassed", 0);
        set { PlayerPrefs.SetInt("PathPassed", value); }
    }


    public static int GetMissionProgress(string mission)
    {
        switch (mission)
        {
            case "Game":
                return GamePlayed;

            case "Obstacle":
                return ObstacleDestroyed;

            case "Path Tile":
                return PathPassed;

            default:
                return 0;
        }
    }

    private void OnEnable()
    {
        Actions.PlayGame += () => { GamePlayed++; };
        Actions.PassRoad += () => { PathPassed++; };
    }

    private void OnDisable()
    {
        Actions.PlayGame -= () => { GamePlayed++; };
        Actions.PassRoad -= () => { PathPassed++; };
    }
    

}

public class MissionSavedData
{
    public MissionTypeData MissionTypeData;

    public int Progress;

    public int Level;

    public int CollectedLevel;
}