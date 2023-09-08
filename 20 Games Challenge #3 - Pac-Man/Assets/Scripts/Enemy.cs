using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] normalSprites;
    public Sprite[] edibleSprites;
    private SpriteRenderer spriteRenderer;
    private int x_coordinates;
    private int y_coordinates;
    private GridSystem gridSystem;
    public Player.Direction direction;
    public float y_axisPerUnit;
    public float x_axisPerUnit;
    private AStar aStarAlgo;
    private float moveTimer;
    public Player pl;
    public bool isArriveToPlayer;
    private GridMap gridMap;


    public void SetXCoordinate(int value)
	{
        x_coordinates = value;
	}
    public void SetYCoordinates(int value)
	{
        y_coordinates = value;
	
    }

    public void SetUpGridSystem(GridSystem grid)
	{
        gridSystem = grid;
	}
    public void SetUpGridMAp(GridMap grid)
	{
        gridMap = grid;
	}
    public void SetPlayer(Player player)
	{
        pl = player;
	
    }


	private void OnEnable()
	{
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        moveTimer = 0;
        isArriveToPlayer = false;
    }

    public void CreateNewaStar()
	{
        aStarAlgo = new AStar(gridSystem.getArray());
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            moveTimer = UpdateTime(moveTimer);
        if (moveTimer < 0 && !isArriveToPlayer)
		{
            MoveEnemy(new Vector2Int(pl.x_coordinate, pl.y_coordinate));
		}
    }
    private float UpdateTime(float moveTimer)
    {
        moveTimer = moveTimer - Time.deltaTime;
        return moveTimer;
    }
    private void MoveEnemy(Vector2Int dest)
	{
        Stack<Node> aStarPath;
        if (pl.havePowerUp)
		{
            //When power up is opened, go to center of board.
            aStarPath = aStarAlgo.FindPath(new Vector2Int(x_coordinates, y_coordinates), new Vector2Int(10, 10));
		}
        else
            aStarPath = aStarAlgo.FindPath(new Vector2Int(x_coordinates, y_coordinates), dest);
        Node nextMove = aStarPath.Pop();
        DeterminateMoveDirection(nextMove);
        gridSystem.setValue(x_coordinates, y_coordinates, 0); //Clears the previous spot.
        Vector3 pos = this.transform.position;
        switch (direction)
        {
            case Player.Direction.DOWN:
                y_coordinates--;
                pos.y = pos.y - y_axisPerUnit;
                break;
            case Player.Direction.UP:
                y_coordinates++;
                pos.y = pos.y + y_axisPerUnit;
                break;
            case Player.Direction.RIGHT:
                pos.x = pos.x + x_axisPerUnit;
                x_coordinates++;
                break;
            case Player.Direction.LEFT:
                x_coordinates--;
                pos.x = pos.x - x_axisPerUnit;
                break;
        }
		if (gridSystem.getValue(x_coordinates, y_coordinates) == 4)
		{
            gridMap.tileMap.SetTile(new Vector3Int(x_coordinates,y_coordinates,0), null);
            pl.score = pl.score - 100;
            gridMap.numOfScores--;
		}
        this.transform.position = pos;
        if(pos == pl.gameObject.transform.position)
		{
            isArriveToPlayer = true;
            //Finish game.
            pl.isAlive = false;
            gridMap.sceneManager.ActivateGameOverMenu();
            Destroy(pl.gameObject);
        }
        gridSystem.setValue(x_coordinates, y_coordinates, 3); //Sets the value for currrent position.
        ChangePlayerSprite();
        moveTimer = 0.75f;
    }

    private void DeterminateMoveDirection(Node moveNode)
	{
        Vector2Int temp = moveNode.position;
        if (temp.x == x_coordinates - 1)
            direction = Player.Direction.LEFT;
        else if (temp.x == x_coordinates + 1)
            direction = Player.Direction.RIGHT;
        else if (temp.y == y_coordinates - 1)
            direction = Player.Direction.DOWN;
        else if (temp.y == y_coordinates + 1)
            direction = Player.Direction.UP;
	}

    public void ChangePlayerSprite()
    {
		if (pl.havePowerUp)
		{
            switch (direction)
            {
                case Player.Direction.DOWN:
                    spriteRenderer.sprite = edibleSprites[0];
                    break;
                case Player.Direction.UP:
                    spriteRenderer.sprite = edibleSprites[3];
                    break;
                case Player.Direction.RIGHT:
                    spriteRenderer.sprite = edibleSprites[1];
                    break;
                case Player.Direction.LEFT:
                    spriteRenderer.sprite = edibleSprites[2];
                    break;
            }
        }
		else
        {
            switch (direction)
            {
                case Player.Direction.DOWN:
                    spriteRenderer.sprite = normalSprites[0];
                    break;
                case Player.Direction.UP:
                    spriteRenderer.sprite = normalSprites[3];
                    break;
                case Player.Direction.RIGHT:
                    spriteRenderer.sprite = normalSprites[1];
                    break;
                case Player.Direction.LEFT:
                    spriteRenderer.sprite = normalSprites[2];
                    break;
            }

        }
    }


}
