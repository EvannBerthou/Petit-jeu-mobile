using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelManager : MonoBehaviour {

    [SerializeField] private int level;
    [SerializeField] private int[] levelsStep;

    [SerializeField] private int currentExperience = 0;
    public int CurrentExperience {
        get {
            return currentExperience;
        }
        set {
            currentExperience = value;
            CheckLevelUp();
        }
    }

    [SerializeField] private ShopManager shop;
    [SerializeField] private Image sliderXP;
    [SerializeField] private TextMeshProUGUI LevelText;

    void Start() {
        sliderXP.fillAmount = 0;
    }

    private void CheckLevelUp() {
        if(level == levelsStep.Length) {
            sliderXP.fillAmount = 1;
            return;
        }
        
        float ratio = (float)currentExperience / levelsStep[level];

        if(ratio >= 1)
            LevelUp();
        else
            sliderXP.fillAmount = ratio;
    }

    private void LevelUp() {
        float remainingXp = currentExperience - levelsStep[level];
        level++;
        CurrentExperience = (int)remainingXp;
        shop.LevelUp(level);
    }
}