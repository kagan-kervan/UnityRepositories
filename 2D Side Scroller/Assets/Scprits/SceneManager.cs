using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    public GameObject tileObject;
    public GameObject[] dayBackgroundObjects;
    public GameObject[] eveningBackgroundObjects;
    public GameObject[] nightBackgroundObjects;
    public Camera mainCamera;
    public GameObject playerObject;


    private int skinindex;
    // Start is called before the first frame update
    void Start()
    { 
       BackgroundRandomizer();
        TileDrawing(-18f, -4.3f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        CameraTracker();
    }

    public void BackgroundRandomizer()
	{
        int random = Random.Range(0, 3);
		switch (random)
		{
            case 0:
				for (int i = 0; i < dayBackgroundObjects.Length; i++)
				{
                    dayBackgroundObjects[i].SetActive(true);
				}
				for (int i = 0; i < eveningBackgroundObjects.Length; i++)
				{
                    eveningBackgroundObjects[i].SetActive(false);  
				}
				for (int i = 0; i < nightBackgroundObjects.Length; i++)
				{
                    nightBackgroundObjects[i].SetActive(false);
				}
                return ;
            case 1:
                for (int i = 0; i < dayBackgroundObjects.Length; i++)
                {
                    dayBackgroundObjects[i].SetActive(false);
                }
                for (int i = 0; i < eveningBackgroundObjects.Length; i++)
                {
                    eveningBackgroundObjects[i].SetActive(true);
                }
                for (int i = 0; i < nightBackgroundObjects.Length; i++)
                {
                    nightBackgroundObjects[i].SetActive(false);
                }
                return ;
            case 2:
                for (int i = 0; i < dayBackgroundObjects.Length; i++)
                {
                    dayBackgroundObjects[i].SetActive(false);
                }
                for (int i = 0; i < eveningBackgroundObjects.Length; i++)
                {
                    eveningBackgroundObjects[i].SetActive(false);
                }
                for (int i = 0; i < nightBackgroundObjects.Length; i++)
                {
                    nightBackgroundObjects[i].SetActive(true);
                }
                return ;
        }
	}

    public void TileDrawing(float startpos_x,float startpos_y, float lengthoftile)
	{
		for (int i = 0; i < 60; i++)
		{
            Vector3 tileposition = new Vector3(startpos_x+(lengthoftile*i),startpos_y, 1);
            Instantiate(tileObject, tileposition, tileObject.transform.rotation);
		}
	}

    public void CameraTracker()
	{
        Vector2 playerPos = playerObject.transform.position;
        playerPos.y = mainCamera.transform.position.y;
        mainCamera.transform.position = playerPos;
	}
    
   

}
