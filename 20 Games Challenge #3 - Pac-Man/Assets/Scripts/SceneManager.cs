using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	public GridMap gridMap;
	public GameObject mainMenuObj;
	public GameObject finishedGameObj;
	public GameObject gameOverObj;
	public TextMeshProUGUI gameOverscoreText;
	public TextMeshProUGUI gameFinishedcoreText;

	public void OnPlayButtonTriggered()
	{
		mainMenuObj.SetActive(false);
		gridMap.SetBoardActive();
		gridMap.CreateLevel();
	}

	public void OnBackButtonPressed()
	{
		mainMenuObj.SetActive(true);
		gameOverObj.SetActive(false);
		finishedGameObj.SetActive(false);
	}

	public void EditScoreText(TextMeshProUGUI scoreText, int score)
	{
		scoreText.text = "Your Score: " + score;
	}

	public void ActivateFinishedGameMenu()
	{
		EditScoreText(gameFinishedcoreText,gridMap.playerObj.GetComponent<Player>().score);
		gridMap.SetBoardInactive();
		finishedGameObj.SetActive(true);
	}
	public void ActivateGameOverMenu()
	{

		EditScoreText(gameOverscoreText, gridMap.playerObj.GetComponent<Player>().score);
		gridMap.SetBoardInactive();
		gameOverObj.SetActive(true);

	}

	public void OnExitButtonPressed()
	{
		Application.Quit();
	}

}
