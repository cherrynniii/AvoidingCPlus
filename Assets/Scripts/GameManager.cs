using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int finalScore = 0;

    [SerializeField]
    private TextMeshProUGUI totalScoreText; // 점수 텍스트

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void IncreaseFinalScore(int value) {
        finalScore += value;
        totalScoreText.SetText(finalScore.ToString());
    }

    public int GetFinalScore() {
        return finalScore;
    }
}
