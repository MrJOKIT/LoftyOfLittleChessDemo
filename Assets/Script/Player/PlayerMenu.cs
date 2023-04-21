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
    private SoundManager soundManager;

    [SerializeField] private Animator bookAnimator;
    //public GameObject menuPanel;

    
    private void Awake()
    {
        _menuActive = false;
        playerMenu.SetActive(false);
    }
    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
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
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        _playerHealth.IsDead = false;
        _playerHealth.Health = _playerHealth.MaxHelath;
        Time.timeScale = 1f;
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync("World");
    }
    public void GetExitPress()
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        Time.timeScale = 1f;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Option(GameObject optionPanel)
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        _menuActive = false;
        playerMenu.SetActive(false);
        optionPanel.SetActive(true);
        Cursor.visible = true;
    }
    
    public void BackMenu(GameObject thisMenu)
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        thisMenu.SetActive(false);
        
    }
    
    public void OpenMenu()
    {
        SoundManager.instace.Play(SoundManager.SoundName.BookOpen);
        Cursor.visible = true;
        _menuActive = true;
        bookAnimator.SetBool("Open",true);
    }

    public void CloseMenu()
    {
        Cursor.visible = false;
        _menuActive = false;
        bookAnimator.SetBool("Open",false);
    }
}
