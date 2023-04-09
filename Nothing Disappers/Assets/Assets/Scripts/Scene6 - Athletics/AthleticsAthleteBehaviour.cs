using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleticsAthleteBehaviour : MonoBehaviour
{
    private Rigidbody2D rgb;
    public float jumpingConstant;
    public float speedMultiplier;
    public bool groundCheck = true;
    [SerializeField] private float flowingGravityScale;
    [SerializeField] private float normalGravityScale;
    public SceneManager scene;
    // Start is called before the first frame update
    void Start()
    {
        rgb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
		if (Input.GetMouseButton(0) && groundCheck)
		{
            Jump();
		}
    }

    private void Movement()
	{
        Vector2 velo = rgb.velocity;
        velo.x = speedMultiplier;
        rgb.velocity = velo;
        if (rgb.velocity.y < 0)
            rgb.gravityScale = flowingGravityScale;
        else
            rgb.gravityScale = normalGravityScale;
	}
    private void Jump()
	{
        rgb.AddForce(new Vector2(0, jumpingConstant*20f));
        groundCheck = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.collider.tag == "Obstacle")
        {
            scene.GoLoseMenu();
        }
        else if (collision.collider.tag == "Finish")
        {
            scene.ActiveTransition();
        }
        else
            groundCheck = true;
	}
}
