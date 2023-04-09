using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallBehaviour : MonoBehaviour
{
    private Rigidbody2D rgb;
    [SerializeField] private float speedMulti;
    private float sceneDelay;
    [SerializeField] private bool isInPong;
    public SceneManager scene;
    // Start is called before the first frame update
    void Start()
    {
        rgb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

		if (EndGameCheck() && isInPong)
		{
			if (this.transform.position.y > 0)
			{
                scene.ActiveTransition();
			}
			else
			{
                scene.GoLoseMenu();
			}
        }
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
        PongPlayerBenaviour player = collision.collider.GetComponent<PongPlayerBenaviour>();
        PongEnemyBehaviour enemy = collision.collider.GetComponent<PongEnemyBehaviour>();
        if (player != null || enemy != null)
        {
            rgb.velocity = rgb.velocity * speedMulti;
            RandomAngle(collision.collider.gameObject);
        }
        else if (collision.collider.GetComponent<BreakoutBrick>() != null)
            collision.collider.GetComponent<BreakoutBrick>().TakeDamage();
    }
    private void RandomAngle(GameObject obj)
    {
        float objectpos = obj.transform.position.x;
        float ballpos = this.transform.position.x;
        float rand_range = Mathf.Abs(objectpos - ballpos);
        float rand_value = Random.Range((rand_range), (rand_range) * 6.2f);
        if (ballpos < objectpos)
            rand_value = -rand_value;
        Vector2 newVelo = new Vector2(rand_value, rgb.velocity.y);
        rgb.velocity = newVelo;

    }

    private bool EndGameCheck()
	{
        if(!isInPong)
                return false;
        if (Mathf.Abs(this.transform.position.y) > 5.3f)
            return true;
        return false;
	}
}
