using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    private Vector2 movePos;
    private Vector2 startPos;

    public float moveFreq;
    public float moveDis;

    public bool moveHorizontal;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (moveHorizontal)
        {
            movePos.x = startPos.x + Mathf.Sin(Time.time * moveFreq) * moveDis;
            transform.position = new Vector2(movePos.x, transform.position.y);
        }
        else
        {
            movePos.y = startPos.y + Mathf.Sin(Time.time * moveFreq) * moveDis;
            transform.position = new Vector2(transform.position.x, movePos.y);
        }
    }
}
