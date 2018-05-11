using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatV2 : MonoBehaviour {

    [Header("Values")]
    private int Consumption = 0;
    private int Production = 10;

    public int consumption {
        get {
            return Consumption;
        }
        set {
            Consumption = value;
            CheckMalus();
            UpdateTexts();
        }
    }

    public int production {
        get {
            return Production;
        }

        set {
            Production = value;
            CheckMalus();
            UpdateTexts();
        }
    }
    public float Ratio {
        get {
            return (float)Consumption / Production;
        }
    }



    [Header("UI")]
    [SerializeField] private TextMeshProUGUI DisplayText;
    [SerializeField] private Image FillSlider;

    private void Start() {
        UpdateTexts();
    }

    private void UpdateTexts() {
        FillSlider.fillAmount = Mathf.Clamp01(Ratio);
        FillSlider.sprite = StatManager.instance.GetSprite(Ratio);
        DisplayText.text = string.Format("{0}/{1}", Consumption, Production);
    }

    public virtual void CheckMalus() { }
}