using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("Save Slots")]
    public Button[] saveButtons; // Assign in Inspector

    [Header("Save Delete Buttons")]
    public GameObject[] deleteButtons; // Assign in Inspector

    [Header("Popups")]
    public ConfirmationPopup saveConfirmationPopup; // Assign in Inspector
    public ConfirmationPopup deleteConfirmationPopup; // Assign in Inspector

    [Header("Player GameObject")]
    public PlayerSaver playerSaver; // Assign your Player GameObject in Inspector

    private void Start()
    {
        CheckSavesAndUpdateUI();
    }

    private void OnEnable()
    {
        CheckSavesAndUpdateUI();
    }

    public void CheckSavesAndUpdateUI()
    {

        for (int i = 0; i < saveButtons.Length; i++)
        {
            int slot = i + 1; // Assuming slot numbers start at 1
            string profileName = $"PlayerSaveData_Slot{slot}";

            bool saveExists = SaveManager.SaveExists(profileName);

            // Assuming each button has a child with a Text component
            TextMeshProUGUI buttonText = saveButtons[i]?.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = saveExists ? $"Save {slot}" : $"Slot {slot}";
            }
            else
            {
                Debug.LogWarning($"No TextMeshProUGUI component found for save button at index: {i}. This button might be configured incorrectly.");
            }

            // Check if 'deleteButtons' is initialized and has enough elements before accessing
            if (deleteButtons != null && deleteButtons.Length > i)
            {
                deleteButtons[i].SetActive(saveExists); // Show or hide delete button based on save existence
            }
        }

    }

    public void OnSaveSlotButtonClick(int slot)
    {
        if (SaveManager.SaveExists($"PlayerSaveData_Slot{slot}"))
        {
            saveConfirmationPopup.Show(() => ConfirmSave(slot));
        }
        else
        {
            ConfirmSave(slot);
        }
    }

    void ConfirmSave(int slot)
    {
        playerSaver.SavePlayerPosition(slot);
        CheckSavesAndUpdateUI();
    }

    public void LoadPlayerSave(int saveSlot)
    {
        string profileName = $"PlayerSaveData_Slot{saveSlot}";
        try
        {
            var loadedSave = SaveManager.Load<PlayerSaveData>(profileName);
            playerSaver.transform.position = loadedSave.saveData.position; // Make sure Player has a public method or variable for setting position if needed
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load save from slot {saveSlot}: {ex.Message}");
        }
    }

    public void OnDeleteSaveButtonClick(int slot)
    {
        // Assuming 'deleteConfirmationPopup' is your popup instance for confirming deletions
        deleteConfirmationPopup.Show(() => ConfirmDelete(slot));
    }

    private void ConfirmDelete(int slot)
    {
        string profileName = $"PlayerSaveData_Slot{slot}";
        try
        {
            SaveManager.Delete(profileName);
            Debug.Log($"Save slot {slot} has been successfully deleted.");
            CheckSavesAndUpdateUI(); // Refresh UI to reflect the deletion
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to delete save from slot {slot}: {ex.Message}");
        }
    }
}