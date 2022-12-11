using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    public enum States { MOVING, ATTACK, DEATH };
    public States enemyStates;
    public abstract void Movement();
    public abstract void Attack();
    public abstract void TakeHit();
}
