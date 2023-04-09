using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowboardPlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float xAxisConstant;
    private Rigidbody2D rgb;
    [SerializeField] private GameObject cameraObj;
    public SceneManager scene;
    // Start is called before the first frame update
    void Start()
    {
        rgb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
            xAxisConstant = -xAxisConstant;
		}
        MovePlayer();
        CameraTracker();
    }

    private void MovePlayer()
	{
        Vector2 forceVector = new Vector2(xAxisConstant, -1);
        transform.Translate(forceVector*Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Obstacle")
		{
            scene.GoLoseMenu();
		}
        else if(collision.collider.tag == "Finish")
		{
            scene.ActiveTransition();
		}
	}

    private void CameraTracker()
	{
        float y_axis = this.transform.position.y / 2;
        Vector3 newPos = cameraObj.transform.position;
        newPos.y = y_axis;
        newPos.z = -10;
        cameraObj.transform.position = newPos;
	}
}
