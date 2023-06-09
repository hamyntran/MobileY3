using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopData : MonoBehaviour
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
    List<ShopItemData> shopItemDatas = new List<ShopItemData>();
    public List<ShopItemData> ShopItemDatas => shopItemDatas;

    #endregion

    #region Public Method

    public ShopItemData GetShopItem(int id)
    {
        return shopItemDatas[id];
    }
    
    #endregion
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
            File.WriteAllText("Assets/Project/Datas/ShopData.txt", sData);
            Debug.Log("Level load successed");
        }
        else
        {
            Debug.LogError("Level load failed");
        }
    }
    
    public void ReadLocalData(string str)
    {
        shopItemDatas.Clear();
        List<Dictionary<string, string>> lst = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(str);
        if (lst != null && lst.Count > 0)
        {
            foreach (var data in lst)
            {
                if (!string.IsNullOrEmpty(data[Const.Key_Category]))
                {
                    ShopItemData lvData = new ShopItemData(data);
                    shopItemDatas.Add(lvData);
                }
            }
        }
    }


    #region Custom Inspector

#if UNITY_EDITOR
    [CustomEditor(typeof(ShopData))]
    public class LoadLevelDataFromGSheet : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ShopData control = (ShopData)target;
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
public class ShopItemData
{
   public CATEGORY Category;
    [FormerlySerializedAs("itemName")] public string ItemName;
    [FormerlySerializedAs("price")] public int Price;

    public ShopItemData(Dictionary<string, string> data)
    {
        if (!string.IsNullOrEmpty(data[Const.Key_Category]))
        {
            string name = data[Const.Key_Category];
            switch (name)
            {
                case Const.Character:
                    Category = CATEGORY.Character;
                    break;
                case Const.Background:
                    Category = CATEGORY.Background;
                    break;
            }
        }
        if (!string.IsNullOrEmpty(data[Const.Key_Item_name]))
        {
            ItemName = data[Const.Key_Item_name];
        }
        
        if (!string.IsNullOrEmpty(data[Const.Key_Price]))
        {
            Price = int.Parse(data[Const.Key_Price]);
        }
    }
    
    private CATEGORY GetCategory(string rank)
    {
        switch (rank)
        {
            case Const.Character:
                return CATEGORY.Character;
            case Const.Background:
                return CATEGORY.Background;
        }

        return CATEGORY.None;
    }
}

public enum CATEGORY
{
    None =0,
    Character = 1,
    Background = 2
}

