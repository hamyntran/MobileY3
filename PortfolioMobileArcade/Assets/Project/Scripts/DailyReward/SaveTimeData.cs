using UnityEditor;
using UnityEngine;
using System;

public class SaveTimeData : SingletonDontDestroy<SaveTimeData>
{
    // private DateTime 
    private string _lastTimeExit
    {
        get
        {
            var test = DateTime.UtcNow.AddDays(-1);
         return PlayerPrefs.GetString("LastTimeExit", test.ToString());
        }
        set { PlayerPrefs.SetString("LastTimeExit", value); }
    }

    public static int DailyStrike
    {
        get => PlayerPrefs.GetInt("DailyStrike", 0);
        set { PlayerPrefs.SetInt("DailyStrike", value); }
    }

    public void OnApplicationQuit()
    {
        _lastTimeExit = DateTime.UtcNow.ToString();
    }

    private void Start()
    {
        DateTime lastExitTime = DateTime.Parse(_lastTimeExit);
        TimeSpan timeSinceLastExit = DateTime.UtcNow - lastExitTime;
        if (timeSinceLastExit.Days == 1)
        {
            PassedOneDay();
        }
        else if (timeSinceLastExit.Days > 1)
        {
            DailyStrike = 0;
        }

//        Debug.Log($"{DateTime.UtcNow}. Last login is {timeSinceLastExit.Days} ago. Daily Day: {DailyStrike}");
    }

    private void PassedOneDay()
    {
        DailyStrike++;
        DailyReward.OnOneDayPassed?.Invoke();
    }

    private void Test()
    {
        DateTime lastExitTime = DateTime.Parse(_lastTimeExit);
        var test = lastExitTime.AddDays(-1);
        lastExitTime = test;
        TimeSpan timeSinceLastExit = DateTime.UtcNow - lastExitTime;

        if (timeSinceLastExit.Days == 1)
        {
            PassedOneDay();
        }
        else if (timeSinceLastExit.Days > 1)
        {
            DailyStrike = 0;
        }

        Debug.Log($"{lastExitTime}. Last login is {timeSinceLastExit.Days} ago. Daily Day: {DailyStrike}");
    }


#if UNITY_EDITOR


    [CustomEditor(typeof(SaveTimeData))]
    public class SaveTimeDataCustomer : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            SaveTimeData control = (SaveTimeData)target;
            if (GUILayout.Button("Exit Game"))
            {
                control.OnApplicationQuit();
            }

            if (GUILayout.Button("Test"))
            {
                control.Test();
            }

            if (GUILayout.Button("Reset Daily stroke"))
            {
                SaveTimeData.DailyStrike = 0;
            }

            if (GUILayout.Button("Add daily strike"))
            {
                SaveTimeData.DailyStrike++;
            }
        }
    }
#endif
}