public class Water : StatV2 {

    public override void CheckMalus() {
        if(consumption > production) {
            int diff = consumption - production;
            ScoreManager.instance.malusWater = diff;
        }
        else
            ScoreManager.instance.malusWater = 0;
    }
}
