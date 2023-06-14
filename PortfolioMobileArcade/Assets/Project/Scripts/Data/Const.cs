﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    #region Shop Data

    //ROW 1 - TITLE
    public const string Key_Category = "Category";
    public const string Key_Item_name = "Item_name";
    public const string Key_Price = "Price";
    
    //COLUMN A - Category
    public const string Character = "Character";
    public const string Background = "Background";
    
    #endregion

    #region Shop Status

    public const string Item_New = "new";
    public const string Item_Bought = "bought";
    public const string Item_Using = "using";
    public const string Item_Unknown = "unknown";


    #endregion

    #region DailyMissionData

    public const string Key_Day = "Day";
    public const string Key_Coin_reward = "Coin_reward";


    #endregion


    #region Mission Type Data

    public const string Key_Mission = "Mission";
    public const string Key_Start_amount = "Start_amount";
    public const string Key_First_reward = "First_reward";
    public const string Key_Amount_gap = "Amount_gap";
    public const string Key_Reward_gap = "Reward_gap";
    public const string Key_Note = "Note";

    #endregion
}