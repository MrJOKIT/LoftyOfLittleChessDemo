using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextStage : MonoBehaviour
{
    public bool isOpen;
    public GameObject button;
    public int sceneNumber;
    private Animator animator;

    private void Start()
    {
        button.SetActive(false);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            SceneManager.LoadScene(sceneNumber);
        }


        if (isOpen)
        {
            animator.SetBool("Open",true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isOpen)
            {
                button.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOpen)
            {
                button.SetActive(false);
            }
        }
    }
}
