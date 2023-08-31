using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] sprites;
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

    public void SetPlayer(Player player)
	{
        pl = player;
	}

	private void OnEnable()
	{
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        moveTimer = 0;
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
		if (moveTimer < 0)
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
        Stack<Node> aStarPath = aStarAlgo.FindPath(new Vector2Int(x_coordinates, y_coordinates), dest);
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
        this.transform.position = pos;
        gridSystem.setValue(x_coordinates, y_coordinates, 3); //Sets the value for currrent position.
        ChangePlayerSprite();
        moveTimer = 0.3f;
    }

    private void DeterminateMoveDirection(Node moveNode)
	{
        Vector2Int temp = moveNode.position;
        if (temp.x == x_coordinates - 1)
            direction = Player.Direction.DOWN;
        else if (temp.x == x_coordinates + 1)
            direction = Player.Direction.UP;
        else if (temp.y == y_coordinates - 1)
            direction = Player.Direction.LEFT;
        else if (temp.y == y_coordinates + 1)
            direction = Player.Direction.RIGHT;
	}

    public void ChangePlayerSprite()
    {
        switch (direction)
        {
            case Player.Direction.DOWN:
                spriteRenderer.sprite = sprites[0];
                break;
            case Player.Direction.UP:
                spriteRenderer.sprite = sprites[0];
                break;
            case Player.Direction.RIGHT:
                spriteRenderer.sprite = sprites[1];
                break;
            case Player.Direction.LEFT:
                spriteRenderer.sprite = sprites[2];
                break;
        }
    }


}
