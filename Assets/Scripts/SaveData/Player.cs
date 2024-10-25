using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int gamesPlayed, gamesWon, gamesLossed;
    public string playerName;
    public int currentHat, currentMask, currentColor;


    public void Save()
    {
        SaveDataManager.SaveData(this);
    }
    public void Load()
    {
        string[] loadededStats = SaveDataManager.LoadData();

        gamesPlayed = Int32.Parse(loadededStats[0]);
        gamesWon = Int32.Parse(loadededStats[1]);
        gamesLossed = Int32.Parse(loadededStats[2]);
        playerName = loadededStats[3];
        currentHat = Int32.Parse(loadededStats[4]);
        currentMask = Int32.Parse(loadededStats[5]);
        currentColor = Int32.Parse(loadededStats[6]);

    }

    public void savePlayerPref()
    {
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
        PlayerPrefs.SetInt("gamesWon", gamesWon);
        PlayerPrefs.SetInt("gamesLossed", gamesLossed);
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt("currentHat", currentHat);
        PlayerPrefs.SetInt("currentMask", currentMask);
        PlayerPrefs.SetInt("currentColor", currentColor);
    }





}
