using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private float time;
    private float timer = 0.4f;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timer)
        {
            Destroy(gameObject);
        }
    }
}
