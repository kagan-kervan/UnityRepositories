using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1366, 768, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene1");
	}

    public void ExitGame()
	{
        Application.Quit();
	}
}
