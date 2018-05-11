using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private LevelManager levelManager;

    private ShopItem[] shopItems;

    void Awake() {
        shopItems = Resources.LoadAll<ShopItem>("ShopItems/");
        shopItems = shopItems.OrderBy(o => o.BasePrice).ToList().ToArray();
        shopUI.Initialize(this);
        shopUI.CreateShopButtons(shopItems);
    }

    public void UpgradeItem(ShopItem item) {
        if(item.GetPrice() <= ScoreManager.instance.Score) {
            ScoreManager.instance.Remove(item.GetPrice());
            item.Level++;
            StatManager.instance.water.consumption += item.WaterUsage;
            StatManager.instance.energy.consumption += item.EnergyUsage;
            shopUI.UpdateItemUI(item);
            UpdateStats(item.categorie, item.ToAdd);
            levelManager.CurrentExperience += item.XP;
        }
    }

    public void SellItem(ShopItem item) {
        if(item.Level > 0) {
            item.Level--;
            ScoreManager.instance.Score += item.SellPrice;
            StatManager.instance.water.consumption -= item.WaterUsage;
            StatManager.instance.energy.consumption -= item.EnergyUsage;
            shopUI.UpdateItemUI(item);
            UpdateStats(item.categorie, -item.ToAdd);
        }
    }

    private ShopItem GetItemByName(string name) {
        return shopItems.Where(x => x.ItemName == name).SingleOrDefault();
    }

    private void UpdateStats(Categorie categorie, float value) {
        switch(categorie) {
            case Categorie.City:
                ScoreManager.instance.UpgradePS(value);
                break;
            case Categorie.Water:
                StatManager.instance.water.production += (int)value;
                break;
            case Categorie.Energy:
                StatManager.instance.energy.production += (int)value;
                break;
            case Categorie.Bonus:
                ScoreManager.instance.ScorePC += value;
                break;
        }
    }

    public void LevelUp(int level) {
        foreach(var item in shopItems) {
            if(item.UnlockLevel == level)
                shopUI.UnlockItem(item);
        }
    }
}