using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager 
{
    private static ScoreManager instance;

    private float score;
    private float newScore;

    private ScoreManager()
    {
        score = 0;
    }

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreManager();
            }
            return instance;
        }
    }

    public void AddScore(float amount)
    {
        score += amount;
    }

    public void SubtractScore(float amount)
    {
        score -= amount;
    }

    public float GetScore()
    {
        return score;
    }

    public void DeleteScore()
    {
        score = newScore;
    }

    public void SaveScore()
    {
        newScore = score;
    }
}

