using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LangueManager : MonoBehaviour {

    public Langues langue;

    public delegate void OnLanguageChange();
    public static event OnLanguageChange onLanguageChange;

    void Awake() {
        if(PlayerPrefs.HasKey("langue"))
            ChangeLanguage(0);
        else
            if(Application.systemLanguage == SystemLanguage.French) ChangeLanguage(0);
        else ChangeLanguage(1);

    }

    public void ChangeLanguage(int i) {
        if(i == (int)Langues.Français)
            Langue.path = Application.streamingAssetsPath + "/Langues/fr.json";
        else if(i == (int)Langues.Anglais)
            Langue.path = Application.streamingAssetsPath + "/Langues/en.json";

        if(Application.platform == RuntimePlatform.WindowsEditor)
            Langue.LoadPC();
        if(Application.platform == RuntimePlatform.Android)
            StartCoroutine(Langue.LoadAndroid(Langue.path));

        PlayerPrefs.SetInt("langue", i);
    }

    void Start() {
        onLanguageChange();
    }
}

interface ILanguage {
    void UpdateGUI();
}

public enum Langues {
    Français,
    Anglais,
}