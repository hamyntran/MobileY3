using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] TextMeshProUGUI priceTMP;

    public void SetSlotUI(string name, int price)
    {
        nameTMP.text = name;
        priceTMP.text = price.ToString();
    }

}
