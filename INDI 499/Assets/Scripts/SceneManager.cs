using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public GameObject gameOverObject;
    public GameObject pl1Object;
    public GameObject pl2Object;
    public GameObject tileObject;
    public GameObject finishLineObj;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI endGametimerText;
    public bool isPL2Active;
    public float timer;
    // Variables for pause menu.
    public bool isPaused;
    public GameObject pauseMenuObject;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1366, 768, true);
        AddPL2();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer(Time.deltaTime);
        PauseInputChecker();
    }
    public void ActivateGameOverMenu()
	{
        endGametimerText.text = "Your time is: "+ timer.ToString("n2");
        gameOverObject.SetActive(true);
        tileObject.SetActive(false);
        finishLineObj.SetActive(false);
        pl1Object.SetActive(false);
        pl2Object.SetActive(false);
        timerText.gameObject.SetActive(false);
	}

    public void PauseInputChecker()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)
			{
                UnPauseGame();
			}
			else
			{
                isPaused = true;
                tileObject.SetActive(false);
                finishLineObj.SetActive(false);
                pauseMenuObject.SetActive(true);
                Time.timeScale = 0;
			}
		}
	}

    public void UnPauseGame()
	{

        isPaused = false;
        tileObject.SetActive(true);
        finishLineObj.SetActive(true);
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartLevel()
	{
        gameOverObject.SetActive(false);
        Vector3 posP1 = new Vector3(5.73f, -1.1974f, -0.2714081f);
        pl1Object.transform.position = posP1;
        pl2Object.transform.position = posP1;
        tileObject.SetActive(true);
        timer = 0;
        timerText.gameObject.SetActive(true);
        finishLineObj.SetActive(true);
        pl1Object.SetActive(true);
        AddPL2();
    }

    public void UpdateTimer(float delta)
	{
        timer = timer + delta;
        timerText.text = "Time: "+timer.ToString("n2");
	}

    public void AddPL2()
	{
        if (PlayerPrefs.GetString("PlayerMode") =="T")
            pl2Object.SetActive(true);
        else
            pl2Object.SetActive(false);
	}

    public void ReturnMenu()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("MENU");
    }

}
