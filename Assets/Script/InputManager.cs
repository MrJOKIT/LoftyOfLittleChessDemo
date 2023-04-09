using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerMenu;
    private bool _menuActive;

    private PlayerHealth _playerHealth;
    // Update is called once per frame

    private void Awake()
    {
        _menuActive = false;
        playerMenu.SetActive(false);
    }

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menuActive && !_playerHealth.IsDead)
        {
            OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menuActive && !_playerHealth.IsDead)
        {
            CloseMenu();
        }
        else
        {
            return;
        }
    }

    public void OpenMenu()
    {
        Cursor.visible = false;
        _menuActive = true;
        playerMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        Cursor.visible = false;
        _menuActive = false;
        playerMenu.SetActive(false);
    }
}
