using System.Collections;
using System.Collections.Generic;
//using UnityEditor.EditorTools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverUI;

    public GameObject menuSet;
    public GameObject inControl;
    public GameObject InOption;
    public GameObject menuBtn;
    public static bool GameIsPaused = false;

    //====NPC====//
    public TalkManager talkManager;
    public bool isAction;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public int talkIndex;
    public GameObject talkPanel;

    public GameObject talkObject;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //esc누르면 메뉴 끄고 키는 기능
        if (!talkObject.activeSelf)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (menuSet.activeSelf)
                {
                    inControl.SetActive(false);
                    InOption.SetActive(false);
                    menuBtn.SetActive(true);
                    menuSet.SetActive(false);
                    Resume();
                }
                else
                {
                    menuSet.SetActive(true);
                    Pause();
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Cancel"))
            {
                isAction = false;
                talkIndex = 0;
                talkObject.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        menuSet.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void Action(GameObject scanObj)
    {

        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        //Visible Talk for Action
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        //End Talk
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        //Continue Talk
        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }
}
