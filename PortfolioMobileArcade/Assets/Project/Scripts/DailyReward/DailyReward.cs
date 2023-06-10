using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private GameObject dailyRewardPanel;
    [SerializeField] private RewardTile rewardTilePrefab;
    [SerializeField] private Transform rewardsContainer;
    private List<RewardData> allRewardDatas = new List<RewardData>();
    public static Action OnOneDayPassed;


    private List<RewardTile> _rewardTiles = new List<RewardTile>();

    private void OnEnable()
    {
        OnOneDayPassed += () => { dailyRewardPanel.SetActive(true); };
        OnOneDayPassed += CheckDailyStroke;
        OnOneDayPassed += SetRewardsStatus;
    }

    /*********** DATA ***********/
    private void Start()
    {
        allRewardDatas = DataManager.Instance.DailyRewardData.AllRewards;

        var orderedData = allRewardDatas.OrderBy(data => data.DayNo);

        foreach (RewardData data in orderedData)
        {
            var newTile = Instantiate(rewardTilePrefab, rewardsContainer);
            _rewardTiles.Add(newTile);
            newTile.Init(data,dailyRewardPanel);
        }
    }

    private void CheckDailyStroke()
    {
        int highestDay = allRewardDatas.Max(x => x.DayNo);
        if (SaveTimeData.DailyStrike > highestDay)
        {
            SaveTimeData.DailyStrike = 1;
        }
    }
    
    private void SetRewardsStatus()
    {
        foreach (RewardTile tile in _rewardTiles)
        {
            if (tile._data.DayNo < SaveTimeData.DailyStrike)
            {
                tile.Status(RewardTile.DAILY_REWARD_STATUS.Passed);
            }
            else if (tile._data.DayNo == SaveTimeData.DailyStrike)
            {
                tile.Status(RewardTile.DAILY_REWARD_STATUS.Today);
            }
            else
            {
                tile.Status(RewardTile.DAILY_REWARD_STATUS.NotYet);
            }
        }
    }
}