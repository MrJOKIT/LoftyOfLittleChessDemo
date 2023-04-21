using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector3 lastCheckPoint;
    public float maxHealthCount;
    public float healthCount;
    public float maxMpCount;
    public float mpCount;
    public float maxUltimatePoint;
    public float ultimatePoint;
    public int deathCount;
    public bool isKing;
    

    public GameData()
    {
        this.maxHealthCount = 100;
        this.healthCount = 100;
        this.maxMpCount = 1;
        this.mpCount = 0;
        this.maxUltimatePoint = 1;
        this.ultimatePoint = 0;
        this.deathCount = 0;
        this.isKing = true;
        lastCheckPoint = Vector3.zero;
        
    }


    /*public int GetPercentageComplete()
    {
        //bossClear
    }*/
}
