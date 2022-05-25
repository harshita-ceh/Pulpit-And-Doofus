using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    public TMP_Text scoreText;

    private int currentScore = 0;

    public void InitScore() {
        scoreText.text = "0";
        currentScore = 0;
    }

    public void UpdateScore(int amount) {
        currentScore += amount;
        scoreText.text = currentScore.ToString();
    }
}
