using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainTheme; // 메인테마 사운드
    public AudioClip menuTheme; // 메뉴 사운드

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioManager.instance.PlayMusic(menuTheme, 2);
        }
        else
        {
            AudioManager.instance.PlayMusic(mainTheme, 3);
        }
    }

    public void PlayMainTheme()
    {
        AudioManager.instance.OffMenuMusic();
        AudioManager.instance.PlayMusic(mainTheme, 3);
    }
}