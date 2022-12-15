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
            Debug.Log("Move left Check.");
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
		else
        {
            Vector3 newPos = transform.position;
            newPos.x += -Mathf.Abs(transform.position.x * moveSpeed * Time.deltaTime)/5;
            transform.position = newPos;
            Debug.Log("Move right Check.");
        }

        if (transform.position.x > 120 || transform.position.x < -120)
        {
            Destroy(gameObject);
        }


    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            player.TakeHit();
        }
        Destroy(gameObject);
    }
}
