using UnityEngine;

public class StatManager : MonoBehaviour {

    #region Singleton
    public static StatManager instance;
    void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [Header("Sprites")]
    [SerializeField] private Sprite FillGreen;
    [SerializeField] private Sprite FillOrange;
    [SerializeField] private Sprite FillRed;

    public Water water;
    public Energy energy;

    public Sprite GetSprite(float ratio) {
        if(ratio >= .90f)
            return FillRed;
        else if(ratio >= .75f && ratio < .90f)
            return FillOrange;
        else
            return FillGreen;
    }

}