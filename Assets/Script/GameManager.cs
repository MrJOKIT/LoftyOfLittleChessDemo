using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public bool isMenu;
    private void Awake()
    {
        
        Cursor.lockState = CursorLockMode.Confined;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        if (!isMenu)
        {
            Cursor.visible = false;
            player.GetComponent<PlayerController>().CanMove = true;
        }

    }
    
}
