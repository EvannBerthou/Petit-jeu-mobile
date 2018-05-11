public class Energy : StatV2 {

    public override void CheckMalus() {
        if(consumption > production) {
            int diff = consumption - production;
            ScoreManager.instance.malusEnergy = diff;
        }
        else
            ScoreManager.instance.malusEnergy = 0;
    }
}
