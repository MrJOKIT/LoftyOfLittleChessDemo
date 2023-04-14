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


    private void Start()
    {
        DisableButtonsDependingOnData();
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
        settingPanel.SetActive(true);
        DeactivateMemu();
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        ActivateMemu();
    }

    public void ExitGame()
    {
        Debug.Log("Exit the game");
        Application.Quit();
    }

    public void OnNewGameClicked()
    {
        saveSlotMenu.ActivateMenu(false);
        this.DeactivateMemu();

    }
    
    public void OnLoadGameClicked()
    {
        saveSlotMenu.ActivateMenu(true);
        this.DeactivateMemu();

    }

    public void OnContinueClciked()
    {
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
}
