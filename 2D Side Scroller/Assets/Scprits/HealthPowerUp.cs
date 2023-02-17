using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

	public PowerUps powerUp;
	public AudioClip powerSFX;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Player player = collision.collider.GetComponent<Player>();
		if (player != null)
		{
			powerUp.Apply(player.gameObject);
			player.audioSource.PlayOneShot(powerSFX);
			Destroy(this.gameObject);
		}
	}
}
