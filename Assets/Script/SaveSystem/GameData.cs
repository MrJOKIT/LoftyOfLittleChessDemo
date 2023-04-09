using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector3 lastCheckPoint;
    public int maxLifeCount;
    public int lifeCount;
    public float maxMpCount;
    public float mpCount;
    public float maxUltimatePoint;
    public float ultimatePoint;
    public int deathCount;
    

    public GameData()
    {
        this.maxLifeCount = 5;
        this.lifeCount = 5;
        this.maxMpCount = 1;
        this.mpCount = 0;
        this.maxUltimatePoint = 1;
        this.ultimatePoint = 0;
        this.deathCount = 0;
        lastCheckPoint = Vector3.zero;
        
    }


    /*public int GetPercentageComplete()
    {
        //bossClear
    }*/
}
