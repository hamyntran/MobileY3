using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
public class DailyRewardData : MonoBehaviour
{
    [Header("GOOGLE SHEET")] [SerializeField]
    private string docID;

    [SerializeField] private int sheetID;
    [SerializeField] private TextAsset textAsset;


    [Space(10)] [Header("LOADED DATA")] [SerializeField]private List<RewardData> allMissions = new List<RewardData>();
    public List<RewardData> AllMissions => allMissions;

    public void LoadData()
    {
        var data = CSVOnlineReader.ReadGSheet(docID, sheetID);

        if (data != null && data.Count > 0)
        {
            string sData = JsonConvert.SerializeObject(data);
            File.WriteAllText("Assets/Project/Datas/DailyRewardData.txt", sData); Debug.Log("Level load successed");
        }
        else
        {
            Debug.LogError("Level load failed");
        }
    }
    
    public void ReadLocalData(string str)
    {
        allMissions.Clear();
        List<Dictionary<string, string>> lst = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(str);
        if (lst != null && lst.Count > 0)
        {
            foreach (var data in lst)
            {
                if (!string.IsNullOrEmpty(data[Const.Key_Day]))
                {
                    RewardData newData = new RewardData(data);
                    allMissions.Add(newData);
                }
            }
        }
    }
    
    
#if UNITY_EDITOR
    [CustomEditor(typeof(DailyRewardData))]
    public class LoadDailyMissionDataFromGSheet : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DailyRewardData control = (DailyRewardData)target;
            if (GUILayout.Button("Load All Data from GSheet"))
            {
                control.LoadData();
                control.ReadLocalData(control.textAsset.text);
            }
        }
    }
#endif
}


[System.Serializable]
public class RewardData
{
    public int DayNo;
    public int CoinReward;


    public RewardData(Dictionary<string, string> data)
    {
        if (!string.IsNullOrEmpty(data[Const.Key_Day]))
        {
            DayNo =  int.Parse(data[Const.Key_Day]);
        }
        
        
        if (!string.IsNullOrEmpty(data[Const.Key_Coin_reward]))
        {
            CoinReward =  int.Parse(data[Const.Key_Coin_reward]);
        }
    }
}
