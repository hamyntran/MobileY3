using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardTile : MonoBehaviour
{
    [SerializeField] private List<Sprite> coinSprites = new List<Sprite>();

    [SerializeField] private Image coinIMG;
    [SerializeField] private Color selectedColor;

    [SerializeField] private TextMeshProUGUI dayTMP;

    [SerializeField] private GameObject tickIMG;

    [SerializeField] private TextMeshProUGUI rewardTMP;

    [SerializeField] private GameObject clamButton;

    public RewardData _data;

    private DAILY_REWARD_STATUS _currentStatus;

    private GameObject _dailyRewardPanel;

    public enum DAILY_REWARD_STATUS
    {
        Passed = 0,
        Today = 1,
        NotYet = 2
    }

    public void Init(RewardData data, GameObject panel)
    {
        _data = data;

        _dailyRewardPanel = panel;

        dayTMP.text = _data.DayNo.ToString();

        rewardTMP.text = $"+{_data.CoinReward.ToString()}";

        if (data.CoinReward > 90)
        {
            coinIMG.sprite = coinSprites[^1];
        } else if (data.CoinReward > 50)
        {
            if (coinSprites.Count >= 2)
            {
                coinIMG.sprite = coinSprites[^2];
            }
            else
            {
                coinIMG.sprite = coinSprites[^1];
            }
        }
        else if (data.CoinReward > 0)
        {
            if (coinSprites.Count >= 3)
            {
                coinIMG.sprite = coinSprites[^3];
            }
            else
            {
                coinIMG.sprite = coinSprites[^2];
            }
        }
        
    }

    public void Claim()
    {
        CoinManager.GainCoin?.Invoke(_data.CoinReward);

        Status(DAILY_REWARD_STATUS.Passed);

        StartCoroutine(nameof(ClosePanel));
    }
    

    public void Status(DAILY_REWARD_STATUS status)
    {
        switch (status)
        {
            case DAILY_REWARD_STATUS.Passed:
                tickIMG.SetActive(true);
                clamButton.SetActive(false);
                rewardTMP.gameObject.SetActive(false);
                _currentStatus = DAILY_REWARD_STATUS.Passed;
                return;

            case DAILY_REWARD_STATUS.Today:
                tickIMG.SetActive(false);
                clamButton.SetActive(true);
                rewardTMP.gameObject.SetActive(false);
                _currentStatus = DAILY_REWARD_STATUS.Today;
                return;
            
            case DAILY_REWARD_STATUS.NotYet:
                tickIMG.SetActive(false);
                clamButton.SetActive(false);
                rewardTMP.gameObject.SetActive(true);
                _currentStatus = DAILY_REWARD_STATUS.NotYet;
                return;
        }
    }

    IEnumerator ClosePanel()
    {
        // Do some time-consuming task
        yield return new WaitForSeconds(0.2f);
        try
        {
            if (_dailyRewardPanel.TryGetComponent(out PlayDotween animation))
            {
                animation.PlayDoTween("Daily Close");
            }
            else
            {
                _dailyRewardPanel.SetActive(false);
            }
        }
        finally
        {
            StopCoroutine(ClosePanel());
        }
    }
    
}