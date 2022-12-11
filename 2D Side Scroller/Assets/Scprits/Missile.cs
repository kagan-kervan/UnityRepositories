using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float moveSpeed = 8f;
    public bool isFiringRight = true;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        if (isFiringRight == true)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
        }
		else
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;

        }

        if (transform.position.x > 20 || transform.position.x < -20)
        {
            Destroy(gameObject);
        }


    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("Collision Check.");
        player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            player.TakeHit();
        }
        Destroy(gameObject);
    }
}
