using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotMenu : Menu
{

    [Header("Menu Navigation")] 
    [SerializeField] private MainMenu mainMenu;
    

    [Header("Menu Button")] 
    [SerializeField] private Button backButton;

    [Header("Confirmation Popup")] 
    [SerializeField] private ConfirmPopUpMenu confirmationPopUpMenu;
    
    private SaveSlot[] saveSlots;

    private bool isLoadingGame = false;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DisableMenuButton();

        if (isLoadingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());
            SaveGameAndLoadScene();
        }
        else if (saveSlot.hasData)
        {
            confirmationPopUpMenu.ActivateMenu(
                "Starting a New Game with this slot will override the currently saved data.Are you sure?",
                () =>
                {
                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());
                    DataPersistenceManager.instance.NewGame();
                    SaveGameAndLoadScene();
                },
                () =>
                {
                    this.ActivateMenu(isLoadingGame);
                }
                );
        }
        else
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());
            DataPersistenceManager.instance.NewGame();
            SaveGameAndLoadScene();
        }
    }

    private void SaveGameAndLoadScene()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("World");
    }
    
    public void OnDeleteClicked(SaveSlot saveSlot)
    {
        DisableMenuButton();
        
        confirmationPopUpMenu.ActivateMenu(
            "Are you sure you want to delete this saved data?",
            () =>
            {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileID());
                ActivateMenu(isLoadingGame);
            },
            () =>
            {
                ActivateMenu(isLoadingGame);
            } 
            );
        
    }
    public void OnReturnClicked()
    {
        mainMenu.ActivateMemu();
        this.DeActivateMenu();
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        gameObject.SetActive(true);

        this.isLoadingGame = isLoadingGame;
        
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        backButton.interactable = true;
        
        GameObject firstSelected = backButton.gameObject;
        foreach (SaveSlot saveSlot in saveSlots )
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null & isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }

        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);
    }

    public void DeActivateMenu()
    {
        gameObject.SetActive(false);
    }

    private void DisableMenuButton()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }

        backButton.interactable = false;
    }
}
