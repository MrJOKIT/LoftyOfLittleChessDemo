using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public bool isOpen;
    private Animator animator;
    private GameStageCheck gameStageCheck;
    public Animator doorAnimator;
    public GameObject button;
    public GameObject monsterNextStage;
    public bool ready = false;
    private bool onTrigger;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameStageCheck = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStageCheck>();
        button.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOpen && ready && onTrigger)
        {
            animator.SetBool("Open",true);
            doorAnimator.SetBool("Open",true);
            gameStageCheck.RemoveSwitch(gameObject);
            monsterNextStage.SetActive(true);
            isOpen = true;
            ready = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && ready)
        {
            button.SetActive(true);
            onTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button.SetActive(false);
            onTrigger = false;
        }
    }

    
}
