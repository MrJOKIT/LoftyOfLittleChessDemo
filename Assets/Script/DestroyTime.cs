using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    private float destroyTime;
    [SerializeField] private float destroyTimeCounter;

    private void Update()
    {
        destroyTime += Time.deltaTime;
        if (destroyTime > destroyTimeCounter)
        {
            Destroy(gameObject);
        }
    }
}
