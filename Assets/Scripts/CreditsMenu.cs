using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    [Header("Menu Objects")]
    public GameObject mainMenu;
    public GameObject creditsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            mainMenu.SetActive(true);
            creditsMenu.SetActive(false);
        }
    }
}