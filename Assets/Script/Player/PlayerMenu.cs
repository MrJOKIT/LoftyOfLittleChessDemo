using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : Menu
{
    [SerializeField] private PlayerHealth _playerHealth;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ReCheckPoint()
    {
        _playerHealth.IsDead = false;
        _playerHealth.Life = _playerHealth.MaxLife;
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
}
