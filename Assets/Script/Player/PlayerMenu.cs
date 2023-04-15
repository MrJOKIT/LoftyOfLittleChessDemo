using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GameObject playerMenu;
    private bool _menuActive;
    private PlayerController _playerController;
    //public GameObject menuPanel;

    
    private void Awake()
    {
        _menuActive = false;
        playerMenu.SetActive(false);
    }
    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menuActive && !_playerHealth.IsDead)
        {
            Cursor.visible = true;
            OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menuActive && !_playerHealth.IsDead)
        {
            Cursor.visible = false;
            
            CloseMenu();
        }
        else
        {
            return;
        }
    }

    public void ReCheckPoint()
    {
        _playerHealth.IsDead = false;
        _playerHealth.Health = _playerHealth.MaxHelath;
        Time.timeScale = 1f;
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync("World");
    }
    public void GetExitPress()
    {
        Time.timeScale = 1f;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Option(GameObject optionPanel)
    {
        _menuActive = false;
        playerMenu.SetActive(false);
        optionPanel.SetActive(true);
        Cursor.visible = true;
    }
    
    public void BackMenu(GameObject thisMenu)
    {
        thisMenu.SetActive(false);
        
    }
    
    public void OpenMenu()
    {
        Cursor.visible = false;
        _menuActive = true;
        playerMenu.SetActive(true);
        //playerMenu.GetComponent<Animator>().SetBool("Open",true);
    }

    public void CloseMenu()
    {
        Cursor.visible = false;
        _menuActive = false;
        playerMenu.SetActive(false);
        //playerMenu.GetComponent<Animator>().SetBool("Open",false);
    }
}
