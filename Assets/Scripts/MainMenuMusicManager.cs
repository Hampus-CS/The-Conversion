using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{
    [Header("Main menu music manager")]
    public AudioSource mainMenuMusic;

    void Start()
    {
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        if (mainMenuMusic != null && !mainMenuMusic.isPlaying)
        {
            mainMenuMusic.Play();
        }
    }

    public void StopMainMenuMusic()
    {
        if (mainMenuMusic != null && mainMenuMusic.isPlaying)
        {
            mainMenuMusic.Stop();
        }
    }

}