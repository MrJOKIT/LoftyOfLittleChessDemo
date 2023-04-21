using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private SaveSlotMenu saveSlotMenu;
    [SerializeField] private Button loadGameButton;
    private SoundManager soundManager;


    private void Start()
    {
        DisableButtonsDependingOnData();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    public void OpenSetting()
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        settingPanel.SetActive(true);
        DeactivateMemu();
    }

    public void CloseSetting()
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        settingPanel.SetActive(false);
        ActivateMemu();
    }

    public void ExitGame()
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        Debug.Log("Exit the game");
        Application.Quit();
    }

    public void OnNewGameClicked()
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        saveSlotMenu.ActivateMenu(false);
        this.DeactivateMemu();

    }
    
    public void OnLoadGameClicked()
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        saveSlotMenu.ActivateMenu(true);
        this.DeactivateMemu();

    }

    public void OnContinueClciked()
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        //DisableMenuButton();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("World");
    }

    private void DisableMenuButton()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
    
    public void ActivateMemu()
    {
        this.gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }
    public void DeactivateMemu()
    {
        this.gameObject.SetActive(false);
    }

    public void LoadScene(int sceneNumber)
    {
        SoundManager.instace.Play(SoundManager.SoundName.UIClick);
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
