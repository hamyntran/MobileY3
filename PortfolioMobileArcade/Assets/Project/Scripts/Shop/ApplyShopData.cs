using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShopData : MonoBehaviour
{
    private List<ShopItemData> _shopItemDatas;
    public List<ShopItemData> BacgroundDatas = new List<ShopItemData>();
    public List<ShopItemData> CharacterDatas = new List<ShopItemData>();

    void Start()
    {
        _shopItemDatas = DataManager.Instance.ShopData.ShopItemDatas;

        foreach (ShopItemData itemData in _shopItemDatas)
        {
            if (itemData.Category == CATEGORY.Background)
            {
                BacgroundDatas.Add(itemData);
            }
            else if (itemData.Category == CATEGORY.Character)
            {
                CharacterDatas.Add(itemData);
            }
        }
    }
}