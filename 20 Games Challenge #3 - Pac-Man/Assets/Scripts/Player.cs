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
    public int x_coordinate;
    public int y_coordinate;
    private float moveTimer = 0f;
    public float powerUpTimer = 0f;
    public bool isAlive;
    public bool havePowerUp;
    public enum Direction
    {
        LEFT, RIGHT, UP, DOWN, NEUTRAL
    };
    public Direction movementDirection;
    public GridSystem grid;
    private GridMap gridMap;
    public int score;
    public void SetGridSystem(GridMap gridObj)
    {
        grid = gridObj.GetGridSystem();
        gridMap = gridObj;
        score = 0;
        isAlive = true;
        havePowerUp = false;
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
    void Update()
    {
        moveTimer = UpdateTime(moveTimer);
        if (powerUpTimer >= 0)
            powerUpTimer = UpdateTime(powerUpTimer);
        if(moveTimer<=0 && isAlive)
            CheckForInputs();
		if (powerUpTimer < 0)
		{
            havePowerUp = false;
		}
    }

private float UpdateTime(float moveTimer)
{
    moveTimer = moveTimer - Time.deltaTime;
    return moveTimer;
}

public void CheckForInputs()
{
    Player.Direction tempdir = Player.Direction.NEUTRAL;
    if (Input.GetAxisRaw("Vertical") > 0)
        tempdir = Player.Direction.UP;
    else if (Input.GetAxisRaw("Vertical") < 0)
        tempdir = Player.Direction.DOWN;
    else if (Input.GetAxisRaw("Horizontal") < 0)
        tempdir = Player.Direction.LEFT;
    else if (Input.GetAxisRaw("Horizontal") > 0)
        tempdir = Player.Direction.RIGHT;
    if (tempdir != Player.Direction.NEUTRAL)
    {
        Vector2Int temp = getCoordinates();
        bool isPossible = isMovementPossible(tempdir,temp);
        if (isPossible)
        {
            MovePlayer(tempdir,temp);
            moveTimer = 0.16f;
        }
    }
}

private bool isMovementPossible(Player.Direction dir, Vector2Int coordinate)
{
    bool flag;
		if (havePowerUp)
		{
			switch (dir)
			{

                case Player.Direction.DOWN:
                    Debug.Log(grid.getValue(coordinate.x, coordinate.y - 1));
                    if (grid.getValue(coordinate.x, coordinate.y - 1) != 0)
                    {
						if (grid.getValue(coordinate.x, coordinate.y - 1) == 3)
						{
                            gridMap.MoveEnemyToCenter();
                            score = score + 3000;
                            flag = true;
						}
                        else if (grid.getValue(coordinate.x, coordinate.y - 1) != 4)
                            flag = false;
                        else
                            flag = true;
                    }
                    else
                        flag = true;
                    break;
                case Player.Direction.UP:
                    Debug.Log(grid.getValue(coordinate.x, coordinate.y + 1));
                    if (grid.getValue(coordinate.x, coordinate.y + 1) != 0)
                    {

                        if (grid.getValue(coordinate.x, coordinate.y + 1) == 3)
                        {
                            gridMap.MoveEnemyToCenter();
                            score = score + 3000;
                            flag = true;
                        }
                        else if (grid.getValue(coordinate.x, coordinate.y + 1) != 4)
                            flag = false;
                        else
                            flag = true;
                    }
                    else
                        flag = true;
                    break;
                case Player.Direction.RIGHT:
                    Debug.Log(grid.getValue(coordinate.x + 1, coordinate.y));
                    if (grid.getValue(coordinate.x + 1, coordinate.y) != 0)
                    {

                        if (grid.getValue(coordinate.x + 1, coordinate.y) == 3)
                        {
                            gridMap.MoveEnemyToCenter();
                            score = score + 3000;
                            powerUpTimer = -1;
                            flag = true;
                        }
                        else if (grid.getValue(coordinate.x + 1, coordinate.y) != 4)
                            flag = false;
                        else
                            flag = true;
                    }
                    else
                        flag = true;
                    break;
                case Player.Direction.LEFT:
                    Debug.Log(grid.getValue(coordinate.x - 1, coordinate.y));
                    if (grid.getValue(coordinate.x - 1, coordinate.y) != 0)
                    {
                        if (grid.getValue(coordinate.x - 1, coordinate.y) == 3)
                        {
                            gridMap.MoveEnemyToCenter();
                            score = score + 3000;
                            flag = true;
                        }
                        else if (grid.getValue(coordinate.x - 1, coordinate.y) != 4)
                            flag = false;
                        else
                            flag = true;
                    }
                    else
                        flag = true;
                    break;
                default:
                    flag = false;
                    break;
            }
		}
    switch (dir)
    {
        case Player.Direction.DOWN:
            if (grid.getValue(coordinate.x, coordinate.y - 1) != 0)
			{
                    if (grid.getValue(coordinate.x, coordinate.y - 1) != 4)
                        flag = false;
                    else
                        flag = true;
            }
            else
                flag = true;
            break;
        case Player.Direction.UP:
            if (grid.getValue(coordinate.x, coordinate.y + 1) != 0)
				{
                    if (grid.getValue(coordinate.x, coordinate.y + 1) != 4)
                        flag = false;
                    else
                        flag = true;
				}
            else
                flag = true;
            break;
        case Player.Direction.RIGHT:
            if (grid.getValue(coordinate.x + 1, coordinate.y) != 0)
				{
                    if (grid.getValue(coordinate.x + 1, coordinate.y) != 4)
                        flag = false;
                    else
                        flag = true;
                }
            else
                flag = true;
            break;
        case Player.Direction.LEFT:
            if (grid.getValue(coordinate.x - 1, coordinate.y )!= 0)
				{
                    if (grid.getValue(coordinate.x - 1, coordinate.y) != 4)
                        flag = false;
                    else
                        flag = true;
				}
            else
                flag = true;
            break;
        default:
            flag = false;
            break;
    }
    return flag;
}

private void MovePlayer(Player.Direction dir, Vector2Int coordinate)
{
    grid.setValue(coordinate.x, coordinate.y, 0); //Clears the previous spot.
    switch (dir)
    {
        case Player.Direction.DOWN:
            coordinate.y--;
            break;
        case Player.Direction.UP:
            coordinate.y++;
            break;
        case Player.Direction.RIGHT:
            coordinate.x++;
            break;
        case Player.Direction.LEFT:
            coordinate.x--;
            break;
    }
    CheckForScoresPlayer(coordinate);
    movementDirection = dir;
    grid.setValue(coordinate.x, coordinate.y, 2); //Sets the value for currrent position.
    MovePlayerFromScene();
    ChangePlayerSprite();
    SetCoordinates(coordinate.x, coordinate.y);

}
 public void CheckForScoresPlayer(Vector2Int pos)
	{
		if (grid.getValue(pos.x, pos.y) == 4)
		{
            gridMap.tileMap.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
            score = score + 100;
            gridMap.numOfScores--;
            System.Random r = new System.Random();
			if (r.Next(640) <= 80)
			{
                havePowerUp = true;
                powerUpTimer = 6.0f;
			}
		}
	}
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
