using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

	public PowerUps powerUp;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Player player = collision.collider.GetComponent<Player>();
		if (player != null)
		{
			powerUp.Apply(player.gameObject);
			Destroy(this.gameObject);
		}
	}
}
