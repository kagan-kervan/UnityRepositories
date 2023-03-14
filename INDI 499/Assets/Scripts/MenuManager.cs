using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject[] carImages;
    public TextMeshProUGUI trackText;
    public TextMeshProUGUI modeText;
    public bool isTwoPlayer = false;
    public int trackSelectionNum;
    public int carSelectionNum;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1366, 768, true);
        PlayerPrefs.SetString("PlayerMode", "F");
        carSelectionNum = 0; 
        trackSelectionNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCarImages();
        UpdateModeText();
        UpdateTrackText();
    }

    public void UpdateCarSelection()
	{
        carSelectionNum++;
        carSelectionNum = carSelectionNum % 4;
	}

    public void UpdateTrackSelection()
	{
        trackSelectionNum++;
        trackSelectionNum = trackSelectionNum % 3;
	}

    public void UpdateModeSelection()
	{
        if (isTwoPlayer)
        {
            PlayerPrefs.SetString("PlayerMode", "F");
            isTwoPlayer = false;

        }
		else
        {
            PlayerPrefs.SetString("PlayerMode", "T");
            isTwoPlayer = true;

        }
	}

    public void UpdateTrackText()
	{
        trackText.text = "TRACK: ";
		switch (trackSelectionNum)
		{
            case 0:
                trackText.text += "Classic Track";
                return;
            case 1:
                trackText.text += "Alternative Track";
                return;
            case 2:
                trackText.text += "Singapore";
                return;
        }
	}

    public void UpdateModeText()
	{
        if (isTwoPlayer)
            modeText.text = "MODE: 2 PLAYER";
        else
            modeText.text = "MODE: TIME ATTACK";
	}

    public void UpdateCarImages()
	{
        PrintCarImage(carSelectionNum);
	}

    public void PrintCarImage(int num)
	{
		for (int i = 0; i < carImages.Length; i++)
		{
            if (i == num)
                carImages[i].gameObject.SetActive(true);
            else
                carImages[i].gameObject.SetActive(false);
		}
	}

    public void ExitGame()
	{
        Application.Quit();
	}

    public void StartTheGame()
	{

        PlayerPrefs.SetInt("CarSelect", carSelectionNum);
        switch (trackSelectionNum)
		{
            case 0:
                UnityEngine.SceneManagement.SceneManager.LoadScene("INDYTRACK");
                return;
            case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene("ISTPARK");
                return;
            case 2:
                UnityEngine.SceneManagement.SceneManager.LoadScene("SINGAPORE");
                return;
        }
	}
}
