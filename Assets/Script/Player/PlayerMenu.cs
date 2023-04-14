using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    private PlayerController _playerController;
    public GameObject menuPanel;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void BackMenu(GameObject thisMenu)
    {
        thisMenu.SetActive(false);
        menuPanel.SetActive(true);
        Cursor.visible = true;
    }
}
