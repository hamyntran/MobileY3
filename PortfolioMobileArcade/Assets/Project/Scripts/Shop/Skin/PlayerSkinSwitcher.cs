
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkinSwitcher : MonoBehaviour
{
    [SerializeField] List<PlayerSkin> _skins = new List<PlayerSkin>();
    private PlayerSkin _currentSkin;

    private void Start()
    {
        foreach (PlayerSkin skin in _skins)
        {
            skin.Init();
            if (skin.CheckInUse())
            {
                _currentSkin = skin;
                _currentSkin.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void OnEnable()
    {
        Shop.OnItemUse += CheckItemCategory;
    }

    private void OnDisable()
    {
        Shop.OnItemUse -= CheckItemCategory;
    }

    private void CheckItemCategory(ShopItemData itemData)
    {
        if (itemData.Category == CATEGORY.Character)
        {
            var skin = _skins.Where(x => x.SkinName == itemData.ItemName).FirstOrDefault();
            if (skin)
            {
                _currentSkin.gameObject.SetActive(false);
                skin.gameObject.SetActive(true);
            }
        }
    }
}