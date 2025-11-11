using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int finalScore = 0;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void IncreaseFinalScore(int value) {
        finalScore += value;
    }

    public int GetFinalScore() {
        return finalScore;
    }
}
