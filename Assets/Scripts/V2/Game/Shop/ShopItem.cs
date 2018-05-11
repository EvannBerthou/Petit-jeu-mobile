using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/New Item")]
public class ShopItem : ScriptableObject {

    public string ItemName;
    public string ItemDescription;

    public float BasePrice;
    public float multiplier = 1.3f;
    public float SellPrice;

    public bool unlocked;
    public int UnlockLevel = 1;
    public int Level;
    public int XP = 1;

    public float ToAdd;
    public int WaterUsage;
    public int EnergyUsage;

    public Categorie categorie;

    public float GetPrice() {
        return Mathf.Round(BasePrice + Mathf.Pow(Level * multiplier, 2));
    }
}

public enum Categorie {
    City,
    Water,
    Energy,
    Bonus
}