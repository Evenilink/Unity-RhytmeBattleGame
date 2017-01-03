using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreControllerUI : MonoBehaviour {
    public RawImage[] healthPlayer1Lifes;
    public RawImage[] healthPlayer2Lifes;

    public Text scorePlayer1;
    public Text scorePlayer2;

    public Texture2D healthFull;
    public Texture2D healthEmpty;

    void Start() {
        scorePlayer1.text = "0";
        scorePlayer2.text = "0";
    }

    public void decreseHealth(int player, int currHealth) {
        if(player == 1)
            healthPlayer1Lifes[currHealth].texture = healthEmpty;
        else
            healthPlayer2Lifes[currHealth].texture = healthEmpty;
    }

    public void increseHealth(int player, int currHealth) {
        if (player == 1)
            healthPlayer1Lifes[currHealth].texture = healthFull;
        else
            healthPlayer2Lifes[currHealth].texture = healthFull;
    }

    public void decreseHealth() {

    }

    public void increaseScore(int player) {

    }
}
