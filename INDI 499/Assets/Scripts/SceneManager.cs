using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject gameOverObject;
    public GameObject pl1Object;
    public GameObject pl2Object;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateGameOverMenu()
	{
        gameOverObject.SetActive(true);
	}

    public void Restart()
	{
        gameOverObject.SetActive(false);
        Vector3 posP1 = new Vector3(5.73f, -1.1974f, -0.2714081f);
        pl1Object.transform.position = posP1;
        pl2Object.transform.position = posP1;
	}
}
