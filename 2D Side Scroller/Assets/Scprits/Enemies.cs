using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    public enum States { MOVING, ATTACK, DEATH };
    public States enemyStates;
    public Player player;
    public GameObject playerObject;
    public void GetPlayerComponent(GameObject obj) //Use it when you create this objects in game mangaer.
    {
        player = obj.GetComponent<Player>();
        playerObject = player.gameObject;
    }
    public abstract void Movement();
    public abstract void Attack();
    public abstract void TakeHit();
}
