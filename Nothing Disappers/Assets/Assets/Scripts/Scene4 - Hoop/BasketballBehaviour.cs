using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballBehaviour : MonoBehaviour
{
    private bool isHeadingLeft = false;
    private bool isLaunched = false;
    [SerializeField] private float speedMultiplier;
    public SceneManager scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!isLaunched)
		{
            BallMovement();
            if (Input.GetMouseButton(0))
            {
                isLaunched = true;
            }
        }
        else
            MoveBallVertical();
    }

    private void BallMovement()
	{
        if (isHeadingLeft)
        {
            Vector2 new_pos = transform.position;
            new_pos.x = new_pos.x - speedMultiplier * Time.deltaTime;
            this.transform.position = new_pos;
        }
		else
		{
            Vector2 new_pos = transform.position;
            new_pos.x = new_pos.x + speedMultiplier * Time.deltaTime;
            this.transform.position = new_pos;
        }
        CheckForMovementRotation();
		
	}

    private void CheckForMovementRotation()
	{
        if(this.transform.position.x >= 7.5f)
		{
            isHeadingLeft = true;
		}
        else if(this.transform.position.x <= -7.5f)
		{
            isHeadingLeft = false;
		}
	}

    private void MoveBallVertical()
	{
         Vector2 newPos = new Vector2(0, speedMultiplier+2);
         transform.Translate(newPos * Time.deltaTime);
        if(this.transform.position.y >= 8f)
		{
            Debug.Log("Lose Game.");
            scene.GoLoseMenu();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("Game finished.");
        scene.ActiveTransition();
	}
}
