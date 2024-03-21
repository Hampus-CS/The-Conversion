using UnityEngine;

public class PlayerSaver : MonoBehaviour
{

    public void SavePlayerPosition(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 3)
        {
            Debug.LogError("Invalid save slot selected. Please select a slot.");
            return;
        }

        string profileName = $"PlayerSaveData_Slot{saveSlot}";
        var playerSave = new PlayerSaveData { position = new Vector2(transform.position.x, transform.position.y), achievements = new int[] { 1, 2, 3, 4, 5 } };
        var saveProfile = new SaveProfile<PlayerSaveData>(profileName, playerSave);
        SaveManager.Save(saveProfile);
    }

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        
        
        var playerSave = new PlayerSaveData {position = Vector2.one * 5f, achievements = new[] {1,2,3,4,5}};
        var saveProfile = new SaveProfile<PlayerSaveData>("PlayerSaveData", playerSave);
        SaveManager.Save(saveProfile);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //load and display data in console
            transform.position = (SaveManager.Load<PlayerSaveData>("playerSaveData").saveData.position);
        }
    }

}
