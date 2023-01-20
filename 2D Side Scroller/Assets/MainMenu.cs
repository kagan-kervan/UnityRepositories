using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartTheGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
	}
	public void QuitGame()
	{
		Debug.Log("QUIT!!");
		Application.Quit();
	}
}
