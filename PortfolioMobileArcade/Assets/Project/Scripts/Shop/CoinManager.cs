using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

/*------------------------------------------
Author: NAME
Last modified by: NAME
-------------------------------------------*/
 
/// <summary>
/// 
/// </summary>

public class CoinManager : SingletonMonoBehaviour<CoinManager>
 {
    #region Properties

    public int Coin
    {
        get => PlayerPrefs.GetInt("Coin", 0);
        set
        {
            if (value > 0)
            {
                PlayerPrefs.SetInt("Coin", value);
            }
            else
            {
                PlayerPrefs.SetInt("Coin", 0);
            }
        }
    }
    #endregion

    public static Action<int> GainCoin;
    #region Unity Callbacks
    private void Start()
    {
        GainCoin += AddCoin;
        Shop.OnItemBought += UseCoin;
    }

    private void OnDestroy()
    {
        Shop.OnItemBought -= UseCoin;
    }

    private void Update()
    {
    
    }
    #endregion
    
    #region Methods

    public void AddCoin(int addition)
    {
        Coin += addition;
    }

    public bool CheckCoin(int price)
    {
        return (price <= Coin);
    }

    public void UseCoin(ShopItemData item)
    {
        Coin -= item.Price;
    }
    
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(CoinManager))]
public class CoinManagerEditorCustom: Editor
{
    public override void OnInspectorGUI()
    {
        CoinManager _target = (CoinManager)target;
        DrawDefaultInspector();
        
        if (GUILayout.Button("Add Coin"))
        {
            _target.AddCoin(10);
            Debug.Log(_target.Coin);
        }
    }
}
#endif
