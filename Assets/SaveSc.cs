using System;
using UnityEngine;

public class SaveSc : MonoBehaviour
{
    public interact score;
    public health health;
    public bool load;

    public void Update()
    {
        if (load)
        {
            LoadData();
            load = false;
        }
    }

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

    public void setLoad()
    {
        load = true;
    }
}