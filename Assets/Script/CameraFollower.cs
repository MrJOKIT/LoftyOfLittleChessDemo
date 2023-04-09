using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothing;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        if (player != null)
        {
            var position = player.transform.position;
            Vector3 newPositon = Vector3.Lerp(transform.position, position + offset, smoothing);
            transform.position = newPositon;
        }
        
    }
}
