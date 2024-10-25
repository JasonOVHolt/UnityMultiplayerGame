using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveDataManager : MonoBehaviour
{


    public static void SaveData(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(player);
        bf.Serialize(stream, data);
        stream.Close();
    }


    public static string[] LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            return data.stats;
        }
        else//////////WHAT happened here
        {
            return new string[0];
        }
    }

}


[Serializable]
public class PlayerData
{
    public string[] stats;

    public PlayerData(Player player)
    {
        stats = new string[7];
        stats[0] = player.gamesPlayed.ToString();
        stats[1] = player.gamesWon.ToString();
        stats[2] = player.gamesLossed.ToString();
        stats[3] = player.playerName;
        stats[4] = player.currentHat.ToString();
        stats[5] = player.currentMask.ToString();
        stats[6] = player.currentColor.ToString();

    }
}
