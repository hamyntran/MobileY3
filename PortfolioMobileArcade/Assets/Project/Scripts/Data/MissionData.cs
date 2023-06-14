using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class MissionData : MonoBehaviour
{
    #region Public Varible

    [Header("GOOGLE_SHEET")]
    [SerializeField]
    string docID;

    [SerializeField] int sheetID;
    [SerializeField] TextAsset data;

    [Space(10)]
    [Header("LOADED DATA")]
    [SerializeField]
    List<MissionTypeData> missionTypeDatas = new List<MissionTypeData>();
    public List<MissionTypeData> MissionTypeDatas => missionTypeDatas;

    #endregion
    
    
    public void LoadData()
    {
        var data = CSVOnlineReader.ReadGSheet(docID, sheetID);
        if (data != null && data.Count > 0)
        {
            string sData = JsonConvert.SerializeObject(data);
            File.WriteAllText("Assets/Project/Datas/MissionData.txt", sData);
            Debug.Log("Level load successed");
        }
        else
        {
            Debug.LogError("Level load failed");
        }
    }
    
    public void ReadLocalData(string str)
    {
        missionTypeDatas.Clear();
        List<Dictionary<string, string>> lst = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(str);
        if (lst != null && lst.Count > 0)
        {
            foreach (var data in lst)
            {
                MissionTypeData lvData = new MissionTypeData(data);
                missionTypeDatas.Add(lvData);
            }
        }
    }
    
    
    #region Custom Inspector

#if UNITY_EDITOR
    [CustomEditor(typeof(MissionData))]
    public class LoadMissionDataFromGSheet : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MissionData control = (MissionData)target;
            if (GUILayout.Button("Load All Data from GSheet"))
            {
                control.LoadData();
                control.ReadLocalData(control.data.text);
            }
        }
    }
#endif

    #endregion
}


[System.Serializable]
public class MissionTypeData
{
    public string MissionType;
    public int StartAmount;
    public int FirstReward;
    public int AmountGap;
    public int RewardGap;
    public string Note;

    public MissionTypeData(Dictionary<string, string> data)
    {
        if (!string.IsNullOrEmpty(data[Const.Key_Mission]))
        {
            MissionType = data[Const.Key_Mission];
        }
        
        if (!string.IsNullOrEmpty(data[Const.Key_Start_amount]))
        {
            StartAmount = int.Parse(data[Const.Key_Start_amount]);
        }
        
        if (!string.IsNullOrEmpty(data[Const.Key_First_reward]))
        {
            FirstReward = int.Parse(data[Const.Key_First_reward]);
        }
        
        if (!string.IsNullOrEmpty(data[Const.Key_Amount_gap]))
        {
            AmountGap = int.Parse(data[Const.Key_Amount_gap]);
        }
        
        if (!string.IsNullOrEmpty(data[Const.Key_Reward_gap]))
        {
            RewardGap = int.Parse(data[Const.Key_Reward_gap]);
        }
        
        if (!string.IsNullOrEmpty(data[Const.Key_Note]))
        {
            Note = data[Const.Key_Note];
        }
    }
}

