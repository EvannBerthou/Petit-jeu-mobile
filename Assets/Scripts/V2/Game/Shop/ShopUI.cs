using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, ILanguage {

    private ShopManager shopManager;
    [SerializeField] private GameObject Shop;

    private bool IsShopOpened;

    [HideInInspector]
    public bool isShopOpened {
        get { return IsShopOpened; }
        set {
            IsShopOpened = value;
            Shop.SetActive(value);
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && isShopOpened)
            isShopOpened = false;
    }

    void Awake() {
        LangueManager.onLanguageChange += UpdateGUI;
    }

    public void Initialize(ShopManager shopManager) {
        this.shopManager = shopManager;
    }

    [SerializeField] private GameObject ItemUIPrefab;
    [SerializeField] private Transform CityParent;
    [SerializeField] private Transform WaterParent;
    [SerializeField] private Transform EnergyParent;
    [SerializeField] private Transform BonusParent;

    [SerializeField] private TextMeshProUGUI CityText;
    [SerializeField] private TextMeshProUGUI WaterText;
    [SerializeField] private TextMeshProUGUI EnergyText;
    [SerializeField] private TextMeshProUGUI BonusText;

    public Dictionary<ShopItem, GameObject> ItemsUI = new Dictionary<ShopItem, GameObject>();

    private int[] ItemCategoriesCount; // 0 : City
                                       // 1 : Water
                                       // 2 : Energy
                                       // 3 : Bonus

    public void CreateShopButtons(ShopItem[] items) {
        ItemCategoriesCount = new int[4];
        foreach(var item in items) {
            GameObject ItemUI = Instantiate(ItemUIPrefab, GetParent(item));
            ItemsUI.Add(item, ItemUI);

            Transform ButtonsParent = ItemsUI[item].transform.GetChild(0).GetChild(0);
            ButtonsParent.Find("Sell Button").GetComponent<Button>().onClick.AddListener(delegate { shopManager.SellItem(item); });
            ButtonsParent.Find("Upgrade Button").GetComponent<Button>().onClick.AddListener(delegate { shopManager.UpgradeItem(item); });

            if(item.unlocked)
                UnlockItem(item);
            else
                LockItem(item);
            ItemCategoriesCount[(int)item.categorie]++;
        }
        SetScrollviewSizes();
    }

    private void SetScrollviewSizes() {
        for(int i = 0; i < 4; i++)
            if(ItemCategoriesCount[i] % 2 != 0) ItemCategoriesCount[i]++;

        CityParent.GetComponent<RectTransform>().sizeDelta = new Vector2(350 / 2 * ItemCategoriesCount[0] + 25, 880);
        WaterParent.GetComponent<RectTransform>().sizeDelta = new Vector2(350 / 2 * ItemCategoriesCount[1] + 25, 880);
        EnergyParent.GetComponent<RectTransform>().sizeDelta = new Vector2(350 / 2 * ItemCategoriesCount[2] + 25, 880);
        BonusParent.GetComponent<RectTransform>().sizeDelta = new Vector2(350 / 2 * ItemCategoriesCount[3] + 25, 880);
    }

    private Transform GetParent(ShopItem item) {
        if(item.categorie == Categorie.City)
            return CityParent;
        else if(item.categorie == Categorie.Water)
            return WaterParent;
        else if(item.categorie == Categorie.Energy)
            return EnergyParent;
        else
            return BonusParent;
    }

    public void UpdateItemUI(ShopItem item) {
        GameObject ItemUI = ItemsUI[item];

        Transform Buttons = ItemUI.transform.GetChild(0).GetChild(0);
        Transform Content = ItemUI.transform.GetChild(0).GetChild(1);
        Transform Stats = Content.Find("Stats");
        Transform Lock = ItemUI.transform.GetChild(0).GetChild(2);

        Content.Find("Name").GetComponent<TextMeshProUGUI>().text = string.Format("{0} ({1})", Langue.GetValue(item.ItemName), item.Level);
        Content.Find("Description").GetComponent<TextMeshProUGUI>().text = Langue.GetValue("D" + item.ItemName);

        Stats.Find("Water").Find("Stat Text").GetComponent<TextMeshProUGUI>().text = item.WaterUsage.ToString();
        Stats.Find("Energy").Find("Stat Text").GetComponent<TextMeshProUGUI>().text = item.EnergyUsage.ToString();
        Stats.Find("Production").Find("Stat Text").GetComponent<TextMeshProUGUI>().text = item.ToAdd.ToString();

        Buttons.GetChild(0).Find("Item Sell Price Text").GetComponent<TextMeshProUGUI>().text = RoundValue.Round(item.SellPrice);
        Buttons.GetChild(1).Find("Item Upgrade Price Text").GetComponent<TextMeshProUGUI>().text = RoundValue.Round(item.GetPrice());

        Lock.Find("Unlock Level Text").GetComponent<TextMeshProUGUI>().text = string.Format("{0} {1}", Langue.textes.Unlock, item.UnlockLevel);
    }

    public void UnlockItem(ShopItem item) {
        item.unlocked = true;
        ItemsUI[item].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
    }
    public void LockItem(ShopItem item) {
        item.unlocked = false;
        ItemsUI[item].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    }

    private void UpdateCategoriesNames() {
        CityText.text = Langue.textes.City;
        WaterText.text = Langue.textes.Water;
        EnergyText.text = Langue.textes.Energy;
        BonusText.text = Langue.textes.Bonus;
    }

    public void UpdateGUI() {
        foreach(var item in ItemsUI) {
            UpdateItemUI(item.Key);
        }
        UpdateCategoriesNames();
    }
}