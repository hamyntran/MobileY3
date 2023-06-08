using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    #region Public Varible
    [Header("       CONFIRM DATA FORM GOOGLE_SHEET                                                                              ")]
    [SerializeField] string docID;
    [SerializeField] int sheetID;
    [SerializeField] TextAsset data;

    [Header("-------------------->USE DATA<---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")]
    [SerializeField] List<Level> levels = new List<Level>();
    public List<Level> Levels => levels;
    #endregion

    #region Public Method
    public Level GetLevel(int id)
    {
        return levels[id];
    }
    /*private void Awake()
    {
        CultureInfo ci = new CultureInfo("en-us");
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;
        if (data == null || string.IsNullOrEmpty(data.text))
        {
            LoadData();
        }
        else
        {
            ReadLocalData(data.text);
        }
    }*/

    public void LoadData()
    {
        var data = CSVOnlineReader.ReadGSheet(docID, sheetID);
        if (data != null && data.Count > 0)
        {
            string sData = JsonConvert.SerializeObject(data);
            File.WriteAllText("Assets/Project/Datas/LevelData.txt", sData);
        }
        else
        {
            //Debug.LogError("Level load failed");
        }
    }

    //---------------------------------------------------------------------------
    public void ReadLocalData(string str)
    {
        levels.Clear();
        List<Dictionary<string, string>> lst = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(str);
        if (lst != null && lst.Count > 0)
        {
            foreach (var data in lst)
            {
                if (!string.IsNullOrEmpty(data[Const.KEY_LEVEL_DATA]))
                {
                    Level lvData = new Level(data);
                    levels.Add(lvData);
                }
            }
        }
    }
    #endregion

    #region Custom Inspector   
#if UNITY_EDITOR
    [CustomEditor(typeof(LevelData))]
    public class LoadLevelDataFromGSheet : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            LevelData control = (LevelData)target;
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
public class Level
{
    public int level;
    public LIMIT eLimit;
    public int limitValue;
    public WARN warn;
    public GoalData goal;
    public List<RewardData> rewards = new List<RewardData>();
    public List<int> menuList = new List<int>();

    public Level(Dictionary<string, string> data)
    {
        if (!string.IsNullOrEmpty(data[Const.KEY_LEVEL_DATA]))
        {
            level = int.Parse(data[Const.KEY_LEVEL_DATA]);
        }
        if (!string.IsNullOrEmpty(data[Const.KEY_LIMIT_DATA]))
        {
            string name = data[Const.KEY_LIMIT_DATA];
            switch (name)
            {
                case Const.PATRONS:
                    eLimit = LIMIT.PATRONS;
                    break;
                case Const.TIME:
                    eLimit = LIMIT.TIME;
                    break;
            }
        }

        if (!string.IsNullOrEmpty(data[Const.KEY_LIMIT_VALUE_DATA]))
        {
            this.limitValue = int.Parse(data[Const.KEY_LIMIT_VALUE_DATA]);
        }

        if (!string.IsNullOrEmpty(data[Const.KEY_WARN_DATA]))
        {
            this.warn = GetWarm(data[Const.KEY_WARN_DATA]);
        }

        if (!string.IsNullOrEmpty(data[Const.KEY_MENU_LIST]))
        {
            string strDataMenu = data[Const.KEY_MENU_LIST];
            string[] strList = strDataMenu.Split(',');

            for (int i = 0; i < strList.Length; i++)
            {
                menuList.Add(int.Parse(strList[i]));
            }
        }

        WARN GetWarm(string name)
        {
            switch (name)
            {
                case Const.BURN:
                    return WARN.BURN;
                case Const.DISCARD:
                    return WARN.DISCARD;
                case Const.ANGRY:
                    return WARN.ANGRY;
                case Const.COIN_STOLE:
                    return WARN.COIN_STOLEN;
            }
            return WARN.NONE;
        }

        goal = new GoalData(data);

        //HANDLE DATA REWARD AND VALUE REWARD
        if (!string.IsNullOrEmpty(Const.KEY_REWARD_DATA))
        {
            string strDataReward = data[Const.KEY_REWARD_DATA];
            string[] strList = strDataReward.Split(',');

            string strDataValue = data[Const.KEY_REWARD_VALUE_DATA];
            string[] strValue = strDataValue.Split(',');
            for (int i = 0; i < strList.Length; i++)
            {
                rewards.Add(new RewardData(strList[i], strValue[i]));
            }
        }
    }
}

[System.Serializable]
public class GoalData
{
    public GOAL eGoal;
    public List<int> goalValues;

    public GoalData(Dictionary<string, string> data)
    {
        if (!string.IsNullOrEmpty(data[Const.KEY_GOAL_DATA]))
        {
            eGoal = GetGoal(data[Const.KEY_GOAL_DATA]);
        }

        goalValues = new List<int>();
        if (!string.IsNullOrEmpty(data[Const.KEY_GOAL_1_STAR_VALUE_DATA]))
        {
            goalValues.Add(int.Parse(data[Const.KEY_GOAL_1_STAR_VALUE_DATA]));
        }

        if (!string.IsNullOrEmpty(data[Const.KEY_GOAL_2_STARS_VALUE_DATA]))
        {
            goalValues.Add(int.Parse(data[Const.KEY_GOAL_2_STARS_VALUE_DATA]));
        }

        if (!string.IsNullOrEmpty(data[Const.KEY_GOAL_3_STARS_VALUE_DATA]))
        {
            goalValues.Add(int.Parse(data[Const.KEY_GOAL_3_STARS_VALUE_DATA]));
        }
    }

    public GOAL GetGoal(string name)
    {
        switch (name)
        {
            case Const.PATRONS_SERVED:
                return GOAL.PATRONS_SERVED;
            case Const.LIKE:
                return GOAL.LIKE;
            case Const.DISH:
                return GOAL.DISH;
            case Const.COINS_EARNED:
                return GOAL.COINS_EARNED;
            case Const.PAYMENT_RECEIVED:
                return GOAL.PAYMENT_RECEIVED;
            case Const.COMBO_BONUSED_AWARDED:
                return GOAL.COMBO_BONUSED_AWARDED;
        }
        return GOAL.NONE;
    }
}

[System.Serializable]
public class RewardData
{
    public REWARD Reward;
    public int value;

    public RewardData(string reward, string value)
    {
        this.Reward = GetReward(reward);
        this.value = int.Parse(value);

        REWARD GetReward(string name)
        {
            switch (name)
            {
                case Const.REWARD_COINS:
                    return REWARD.COINS;
                case Const.REWARD_GEMS:
                    return REWARD.GEMS;
                case Const.REWARD_CHESTS:
                    return REWARD.CHEST;
                case Const.REWARD_WORKERS:
                    return REWARD.WORKER;
            }
            return REWARD.NONE;
        }
    }
}

public enum LIMIT
{
    PATRONS = 0,
    TIME = 1
}
public enum GOAL
{
    LIKE = 0,
    DISH = 1,
    PATRONS_SERVED = 2,
    COINS_EARNED = 3,
    PAYMENT_RECEIVED = 4,
    TIPS_RECEIVED = 5,
    COMBO_BONUSED_AWARDED = 6,
    NONE = 7
}

public enum WARN
{
    BURN = 0,
    DISCARD = 1,
    COIN_STOLEN = 2,
    ANGRY = 3,
    NONE
}

public enum REWARD
{
    COINS = 0,
    GEMS = 1,
    CHEST = 2,
    WORKER = 3,
    NONE = 4
}
