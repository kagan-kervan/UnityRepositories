using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PLAYER_SIDE
    {
        PLAYER1, PLAYER2
    }
    public PLAYER_SIDE pl_side;
    public Rigidbody2D shipBody;
    public float moveSpeed;
    public float LEFT_LAST_POS;
    public float RIGHT_LAST_POS;
    public GameObject missileObject;
    public float missileTimer;
    public float missileOffsetX;
    public float missileOffsetY;
    private float initialMissileTimer;

    // Start is called before the first frame update
    void Start()
    {
        initialMissileTimer = missileTimer;
    }

    // Update is called once per frame
    void Update()
    {
        missileTimer = UpdateTimer(missileTimer, Time.deltaTime);
        if(missileTimer <= 0)
        {
            LaunchMissile();
            missileTimer = initialMissileTimer;
        }
        Movement();

    }

    private void LaunchMissile()
    {
        Vector3 transPosLeft = transform.position;
        transPosLeft.y += missileOffsetY;
        transPosLeft.x -= missileOffsetX;
        Instantiate(missileObject, transPosLeft, transform.rotation);
        Vector3 transPosRight = transform.position;
        transPosRight.y += missileOffsetY;
        transPosRight.x += missileOffsetX;
        Instantiate(missileObject, transPosRight, transform.rotation);
    }

    public float UpdateTimer(float value, float delta)
    {
        return value - delta;
    }
    void Movement()
    { 
        float movementvalue = GetValue();
        PaddleMovement(movementvalue);
    }

    public void PaddleMovement(float value)
    {
        Vector2 velocity = shipBody.velocity;
        velocity.x = value * moveSpeed;
        shipBody.velocity = velocity;
    }

    public float GetValue()
    {
        float value;
        if (pl_side == PLAYER_SIDE.PLAYER1)
            value = Input.GetAxisRaw("Horizontal");
        else
            value = Input.GetAxisRaw("HorizontalPL2");
        if (transform.position.x < LEFT_LAST_POS && value < 0)
            value = 0;
        else if (transform.position.x > RIGHT_LAST_POS && value > 0)
            value = 0;
        return value;
    }

}
