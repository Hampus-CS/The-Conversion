using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public MainMenuMusicManager musicManager;

    public void Play()
    {

        if (musicManager != null)
        {

            musicManager.StopMainMenuMusic();
            SceneManager.LoadScene(1);

        }

    }

    public void Quit()
    {

        Debug.Log("Player has quit the game");
        Application.Quit();

    }

}