using System;
using UnityEngine;

public class SaveSc : MonoBehaviour
{
    public interact score;
    public health health;

    public void SaveData()
    {
        PlayerPrefs.SetInt("score", score.Score);
        PlayerPrefs.SetInt("health", health.healthLevel);
    }

    public void LoadData()
    {
       score.Score = PlayerPrefs.GetInt("score");
       health.healthLevel = PlayerPrefs.GetInt("health");
    }
}