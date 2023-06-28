using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwitcher : MonoBehaviour
{
    public List<Background> Backgrounds = new List<Background>();
    private Camera _camera;
    private Background _currentBG;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        foreach (Background bg in Backgrounds)
        {
            bg.GetItemData();
            if (bg.CheckInUse())
            {
                _currentBG = bg;
                SetBG();
                break;
            }
        }
    }

    private void CheckItemCategory(ShopItemData itemData)
    {
        if (itemData.Category == CATEGORY.Background)
        {
            var bg = Backgrounds.Where(x => x.BGName == itemData.ItemName).FirstOrDefault();
            if (bg != null)
            {
                _currentBG = bg;
                SetBG();
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


    private void SetBG()
    {
        _camera.backgroundColor = _currentBG.Color;
    }

    [System.Serializable]
    public class Background
    {
        public string BGName;
        public Color Color;


        [HideInInspector] public ShopItemData ItemData;
        
        public void GetItemData()
        {
            ItemData = DataManager.Instance.ShopData.GetShopItem(BGName);
            
        }

        public bool CheckInUse()
        {
            if (ItemData == null)
            {
                return false;
            }
            
            return PlayerPrefs.GetString($"{ItemData.ItemName}/{ItemData.Price}") == Const.Item_Using;
        }
    }
}