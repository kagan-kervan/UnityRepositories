using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{

    public Player player1;
    public Player player2;
    public Vector3 initialPos;
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        //Takes the initial position of player's object.
        initialPos = player1.transform.position;
        initialPos.z = -10f;
        mainCam.gameObject.transform.position = initialPos;
       
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCameraPosition();
    }

    public void ChangeCameraPosition()
	{
        Vector3 player1pos = player1.transform.position;
        Vector3 player2pos = player2.transform.position;
        Vector3 camPos;
        if (player1pos.y <= player2pos.y)
            camPos.y = player1pos.y - 2.0f;
        else
            camPos.y = player2pos.y - 2.0f;
        if (player1pos.x <= player2pos.x)
            camPos.x = player2pos.x - 2.0f;
        else
            camPos.x = player1pos.x - 2.0f;
        camPos.z = -10f;
        mainCam.gameObject.transform.position = camPos;
            

	}

    public void ChangeCamSize()
	{
        Vector3 player1pos = player1.transform.position;
        Vector3 player2pos = player2.transform.position;
		if (Mathf.Abs(player1pos.y - player2pos.y) > 10)
		{
            float size = mainCam.orthographicSize;
            size += Mathf.Abs(player1pos.y - player2pos.y) * 0.4f;
            size = Mathf.Min(size, 15);
            mainCam.orthographicSize = size;
		}
    }
}
