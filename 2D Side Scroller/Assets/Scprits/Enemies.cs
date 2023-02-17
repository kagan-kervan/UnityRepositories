using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    public enum States { MOVING, ATTACK, DEATH };
    public States enemyStates;
    public Player player;
    public GameObject playerObject;
    public AudioSource audioSource;
    public AudioClip enemyHitSound;
    public AudioClip enemyDeathSound;
    public void GetPlayerComponent(GameObject obj) //Use it when you create this objects in game mangaer.
    {
        player = obj.GetComponent<Player>();
        playerObject = player.gameObject;
    }
    public abstract void Movement();
    public abstract void Attack();
    public abstract void TakeHit(int hitPoint);

    public GameObject PowerUpDrop(List<GameObject> powerupList)
	{
        GameObject powerUpObject = null;
        int ran = Random.Range(0, 33);
		if (ran > 8)
		{
            int rand_powerUp = Random.Range(0, powerupList.Count);
            powerUpObject = powerupList[rand_powerUp];
		}
        return powerUpObject;
	}
}
