using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InputField : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetString("PlayerName", text.text);
            MusicManager musicManager = FindObjectOfType<MusicManager>();

            if (musicManager != null)
            {
                musicManager.PlayMainTheme();
            }
            SceneManager.LoadScene("Main");
        }
    }
}
