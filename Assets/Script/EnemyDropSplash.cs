using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDropSplash : MonoBehaviour
{
    public GameObject[] item;
    public int minDrop = 1;
    public int maxDrop = 5;
    private int itemNumber;
    private int dropCount;
    private int dropCountCounter;

    private bool dropReady = false;
    private float dropTime;
    private float dropTimeCounter = 0.1f;

    private void Start()
    {
        dropCount = 0;
        dropCountCounter = Random.Range(minDrop, maxDrop);
        dropReady = true;
    }

    private void Update()
    {
        if (dropReady)
        {
            dropTime += Time.deltaTime;
            if (dropTime > dropTimeCounter)
            {
                Instantiate(item[RandomItemNumber()], transform.position, Quaternion.identity);
                dropCount++;
                dropTime = 0;
            }
        }

        if (dropCount >= dropCountCounter)
        {
            dropReady = false;
            Destroy(gameObject);
        }
    }

    public int RandomItemNumber()
    {
        float ramdomNumber = Random.Range(1, 10);
        if (ramdomNumber <= 2)
        {
            return 1;
        }
        else if (ramdomNumber <= 5)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }


    
}
