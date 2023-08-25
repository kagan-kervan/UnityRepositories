using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] pacmanSprites;
    public SpriteRenderer spriteRenderer;
    public float y_axisPerUnit;
    public float x_axisPerUnit;
    private int x_coordinate;
    private int y_coordinate;
    private float moveTimer = 0f;
    public enum Direction
    {
        LEFT, RIGHT, UP, DOWN, NEUTRAL
    };
    public Direction movementDirection;
    public GridMap gridSystem;
    public void SetGridSystem(GridMap gridObj)
    {
        gridSystem = gridObj;
    }

    public Vector2Int getCoordinates()
	{
        Vector2Int temp = new Vector2Int();
        temp.x = x_coordinate;
        temp.y = y_coordinate;
        return temp;
	}
    public void SetCoordinates(int width, int height)
	{
        x_coordinate = width;
        y_coordinate = height;
	}

    // Update is called once per frame
    /*void Update()
    {
        moveTimer = UpdateTime(moveTimer);
        if(moveTimer<=0)
            CheckForInputs();
    }

	private float UpdateTime(float moveTimer)
	{
        moveTimer = moveTimer - Time.deltaTime;
        return moveTimer;
	}

	public void CheckForInputs()
	{
        Direction tempdir = Direction.NEUTRAL;
        if (Input.GetAxisRaw("Vertical") > 0)
            tempdir = Direction.UP;
        else if (Input.GetAxisRaw("Vertical") < 0)
            tempdir = Direction.DOWN;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            tempdir = Direction.LEFT;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            tempdir = Direction.RIGHT;
        if(tempdir != Direction.NEUTRAL)
		{
            bool isPossible = isMovementPossible(tempdir);
            if (isPossible)
			{
                MovePlayer(tempdir);
                moveTimer = 0.32f;
			}
        }
	}

    //Checks if the movement that is inputed possible.
    private bool isMovementPossible(Direction dir)
    {
        bool flag;
        switch (dir)
        {
            case Direction.DOWN:
                Debug.Log(gridSystem.grid.getValue(x_coordinate, y_coordinate - 1));
                if (gridSystem.grid.getValue(x_coordinate, y_coordinate - 1) != 0)
                    flag = false;
                else
                    flag = true;
                break;
            case Direction.UP:
                Debug.Log(gridSystem.grid.getValue(x_coordinate, y_coordinate + 1));
                if (gridSystem.grid.getValue(x_coordinate, y_coordinate + 1) != 0)
                    flag = false;
                else
                    flag = true;
                break;
            case Direction.RIGHT:
                Debug.Log(gridSystem.grid.getValue(x_coordinate+1, y_coordinate));
                if (gridSystem.grid.getValue(x_coordinate + 1, y_coordinate) != 0)
                    flag = false;
                else
                    flag = true;
                break;
            case Direction.LEFT:
                Debug.Log(gridSystem.grid.getValue(x_coordinate-1, y_coordinate));
                if (gridSystem.grid.getValue(x_coordinate - 1, y_coordinate) != 0)
                    flag = false;
                else
                    flag = true;
                break;
            default:
                flag = false;
                break;
        }
        return flag;
    }

    private void MovePlayer(Direction dir)
    {
        gridSystem.grid.setValue(x_coordinate, y_coordinate, 0); //Clears the previous spot.
        switch (dir)
        {
            case Direction.DOWN:
                y_coordinate--;
                break;
            case Direction.UP:
                y_coordinate++;
                break;
            case Direction.RIGHT:
                x_coordinate++;
                break;
            case Direction.LEFT:
                x_coordinate--;
                break;
        }
        movementDirection = dir;
        gridSystem.grid.setValue(x_coordinate, y_coordinate, 2); //Sets the value for currrent position.
        MovePlayerFromScene();
        ChangePlayerSprite();

    }
    */
    public void MovePlayerFromScene()
    {
        Vector3 pos = this.transform.position;
        switch (movementDirection)
        {
            case Direction.DOWN:
                pos.y = pos.y - y_axisPerUnit;
                break;
            case Direction.UP:
                pos.y = pos.y + y_axisPerUnit;
                break;
            case Direction.RIGHT:
                pos.x = pos.x + x_axisPerUnit;
                break;
            case Direction.LEFT:
                pos.x = pos.x - x_axisPerUnit;
                break;
        }
        this.transform.position = pos;
    }
    
    public void ChangePlayerSprite()
    {
        switch (movementDirection)
        {
            case Direction.DOWN:
                spriteRenderer.sprite = pacmanSprites[0];
                break;
            case Direction.UP:
                spriteRenderer.sprite = pacmanSprites[0];
                break;
            case Direction.RIGHT:
                spriteRenderer.sprite = pacmanSprites[1];
                break;
            case Direction.LEFT:
                spriteRenderer.sprite = pacmanSprites[2];
                break;
        }
    }
}
