using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    #region Singleton

    public static ScoreManager instance;

    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [Header("Score")]
    public float Score = 0; //Score actuel
    public float ScorePS = 0; //Score par secondes
    public float ScorePC = 1; //Score par click
    public float malusWater = 0;
    public float malusEnergy = 0;
    public float malus;
    public float TemporaryBonus = 1;
    private float NextScoreToAdd = 0;

    [Header("Textes")]
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI ScorePSText;
    [SerializeField] private GameObject clickText;
    [SerializeField] private Vector3 offset;

    [Space]
    [SerializeField] private AudioClip clip;
    [SerializeField] private Transform Canvas;


    void Update() {
        if(Input.touchCount > 0) {
            for(int i = 0; i < Input.touchCount; i++) {
                if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId) && Input.GetTouch(i).phase == TouchPhase.Began)
                    Add(Input.GetTouch(i).position);
            }
        }
        else if(Input.GetMouseButtonDown(0))
            if(!EventSystem.current.IsPointerOverGameObject())//If i don't click on an UI element		
                Add(Input.mousePosition);
    }

    void FixedUpdate() {
        NextScoreToAdd = (ScorePS - (malusWater + malusEnergy + malus)) * TemporaryBonus;
        Score += NextScoreToAdd * Time.fixedDeltaTime;
        UpdateGUI();
    }

    void UpdateGUI() {
        ScoreText.text = RoundValue.Round(Score);
        ScorePSText.text = RoundValue.Round(NextScoreToAdd);
    }

    public void Add(Vector2 pos) {
        Score += ScorePC;
        ClickText(ScorePC, pos);
        //AudioManager.instance.Play ("money");
    }

    public void ClickText(float value, Vector3 pos) {
        GameObject gm = Instantiate(clickText, Canvas);
        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-offset.x, offset.x), UnityEngine.Random.Range(-offset.y, offset.y), 0);
        gm.transform.position = pos + randomOffset;

        gm.GetComponentInChildren<Text>().text = string.Format("+{0}$", value);
        Destroy(gm, 0.8f);
    }

    public void Remove(float x) {
        Score -= x;
    }

    public void UpgradePS(float value) {
        ScorePS += value;
    }
}