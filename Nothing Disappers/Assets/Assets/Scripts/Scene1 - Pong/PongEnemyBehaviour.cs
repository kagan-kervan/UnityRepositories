using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float moveSpeed;
    private float AIdeadZone = 1f;
    [SerializeField] private GameObject ballObject;
    private Rigidbody2D rgb;
    
    // Start is called before the first frame update
    void Start()
    {
        rgb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ComputerPaddleMovement();
    }
    private void ComputerPaddleMovement()
	{
        Vector2 ballPos = ballObject.transform.position;
        float direction;
        if (Mathf.Abs(this.transform.position.x - ballPos.x) > AIdeadZone)
		{
            if (ballPos.x < this.transform.position.x)
                direction = -1;
            else
                direction = 1;
		}
        else
            direction = 0;
        PaddleMovement(direction);
        if(Random.value < 0.1f)
		{
            speedMultiplier = Random.Range(0.3f, 1f);
		}
        if(this.transform.position.x+direction<-8f || this.transform.position.x+direction > 8f)
		{
            rgb.velocity = new Vector2(0, 0);
		}
	}

    private void PaddleMovement(float value)
	{
        Vector2 velocity = rgb.velocity;
        velocity.x = value * speedMultiplier * moveSpeed;
        rgb.velocity = velocity;
        
	}
}
