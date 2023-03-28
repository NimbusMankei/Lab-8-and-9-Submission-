using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public int score;
    public Vector3 playerPosition;

    // Game Starts with default values when there's no data to load
    public GameData()
    {
        this.score = 0;
        this.playerPosition  = new Vector3(33.88f, 0.4999999f, -8.87f);
    }
}
