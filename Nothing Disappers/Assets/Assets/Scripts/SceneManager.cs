using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public enum Scenes
	{
        Scene1,Scene2,Scene3, Scene4, Scene5, Scene6, Scene7, Scene8, Scene9, Scene10
	}
    public Scenes sceneEnum;
    public GameObject[] allObjects;
    public GameObject transitionObject;
    public float transtitionDelay;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
		if (transitionObject.activeInHierarchy)
		{
            transtitionDelay = UpdateTimer(transtitionDelay);
            if(transtitionDelay<= 0)
			{
                GoToNextScene();
			}
		}
    }

    private void InactiveAllObjects()
	{
		for (int i = 0; i < allObjects.Length; i++)
		{
            if(allObjects[i]!= null)
                allObjects[i].SetActive(false);
		}
	}

    public void ActiveTransition()
	{
        InactiveAllObjects();
        transitionObject.SetActive(true);
	}

    private float UpdateTimer(float time)
	{
        time = time - Time.deltaTime;
        return time;
	}

    private void GoToNextScene()
	{
		switch (sceneEnum)
		{
            case Scenes.Scene1:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene2");
                return;
            case Scenes.Scene2:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene3");
                return;
            case Scenes.Scene3:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene4");
                return;
            case Scenes.Scene4:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene5");
                return;
            case Scenes.Scene5:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene6");
                return;
            case Scenes.Scene6:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene7");
                return;
            case Scenes.Scene7:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene8");
                return;
            case Scenes.Scene8:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene9");
                return;
            case Scenes.Scene9:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene10");
                return;
            case Scenes.Scene10:
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
                return;
        }
	}

    public void GoLoseMenu()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("LostGameMenu");
	}



}
