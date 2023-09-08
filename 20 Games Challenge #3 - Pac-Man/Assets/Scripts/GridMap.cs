using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Tilemap))]
public class GridMap : MonoBehaviour
{
    public int width;
    public int height;
    public int inBoardVerticalWalls;
    public int inBoardHorizontalWalls;
    private GridSystem grid;
    public Tilemap tileMap;
    public TileBase horizontalWallTile;

	public TileBase verticalWallTile;
    public TileBase coinTileBase;
    public TileBase powerUpTileBase;
    public GameObject playerObj;
    public GameObject enemyObj;
    public int[,] gridArray;
	public int numOfScores;
    public SceneManager sceneManager;

	private void OnEnable()
	{
    }

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (isGameFinished() && this.gameObject.activeInHierarchy)
		{
            Debug.Log("Game finished.");
            sceneManager.ActivateFinishedGameMenu();
            Destroy(playerObj);
        }
    }


    public GridSystem GetGridSystem()
	{
        return grid;
	}

    public void CreateLevel()
    {

        grid = new GridSystem(width, height, 64);
        gridArray = grid.getArray();
        numOfScores = 0;
        CreateMap();
        CreatePlayer();
        CreateEnemy(10, 9);
        FillRestOfTheMap();
    }

    public void CreateMap()
	{
		for (int i = 0; i < width; i++) 
		{
            // Set the horizontal wall lines.
            SetWall(i, 0,horizontalWallTile);
            SetWall(i, height - 1,horizontalWallTile);
        }
		for (int i = 0; i < height; i++)
		{
            // Set the vertical wall lines.
            SetWall(0, i,verticalWallTile);
            SetWall(width - 1, i,verticalWallTile);
		}
        int midPointhor = width / 2;
        int midPointver = height / 2;
        SetUpTheCenterArea(midPointhor, midPointver, verticalWallTile);
		for (int i = 0; i < inBoardVerticalWalls; i++)
		{
            Vector2Int coordinates = FindCoordinatesForVerticalWall();
            SetUpInBoardWallVertical(coordinates.x, coordinates.y, verticalWallTile);
		}
		for (int i = 0; i < inBoardHorizontalWalls; i++)
		{
            bool isLeftSide;
            Vector2Int coordinates = FindCoordinatesHorizontalWallLeftSide(out isLeftSide);
            SetUpInBoardWallHorizontal(coordinates.x, coordinates.y, horizontalWallTile, isLeftSide);
		}
        for (int i = 0; i < inBoardHorizontalWalls; i++)
        {
            bool isLeftSide;
            Vector2Int coordinates = FindCoordinatesHorizontalWallRightSide(out isLeftSide);
            SetUpInBoardWallHorizontal(coordinates.x, coordinates.y, horizontalWallTile, isLeftSide);
        }
    }


	private void SetWall(int width, int height, TileBase tile)
	{
        tileMap.SetTile(new Vector3Int(width, height, 0), tile);
        grid.setValue(width, height, 1); // 1 is being used for walls.
    }


	private void FillRestOfTheMap()
	{
		for (int i = 0; i < grid.getWidth(); i++)
		{
			for (int j = 0; j < grid.getHeight(); j++)
			{
                if (grid.getValue(i, j) == 0)
                {
                    CreateScoreTile(i, j);
                    numOfScores++;
                }
			}
		}
	}

	private void CreateScoreTile(int x,int y)
	{
        tileMap.SetTile(new Vector3Int(x, y, 0), coinTileBase);
        grid.setValue(x, y, 4);
	}

	private void SetUpTheCenterArea(int midPointhorizontal, int midPointVertical, TileBase tile)
	{
		for (int i = 0; i < 4; i++)
		{
            //Sets up the other half.
            SetWall(midPointhorizontal - 2, midPointVertical + i,tile);
            SetWall(midPointhorizontal + 2, midPointVertical + i, tile);
        }
		for (int i = 0; i < 2; i++)
		{
            SetWall(midPointhorizontal + i, midPointVertical, tile);
            SetWall(midPointhorizontal - i, midPointVertical, tile);
        }
	}

    private Vector2Int FindCoordinatesForVerticalWall()
	{
        Vector2Int temp = new Vector2Int();
        System.Random r = new System.Random();
        do
        {
            int rand = r.Next(0, 20);
            if (rand < 10)  //Upside
            {
                temp.x = r.Next(0, this.width);
                temp.y = r.Next(this.height/2, this.height);
            }
            else  //Downside
            {
                temp.x = r.Next(0, this.width);
                temp.y = r.Next(3, this.height/2-3);

            }
        } while (grid.getValue(temp.x, temp.y)!= 0 &&
                grid.getValue(temp.x, temp.y - 1) != 0 &&
                grid.getValue(temp.x, temp.y - 2) != 0);
        return temp;
	}

    private void SetUpInBoardWallVertical(int width, int height, TileBase tile)
	{
		for (int i = 0; i < 3; i++)
		{
                SetWall(width, height - 1, tile);
		}
	}

    private Vector2Int FindCoordinatesHorizontalWallLeftSide(out bool isLeftSide)
	{
        Vector2Int temp = new Vector2Int();
        System.Random r = new System.Random();
        isLeftSide = true;
        do
        {
            int rand = r.Next(0, 20);
            if (rand < 10)  //Left sid
            {
                temp.x = r.Next(3, this.width / 2 - 3);
                temp.y = r.Next(0, this.height);
            }
            else  //Right side
            {
                temp.x = r.Next(this.width / 2 + 3, this.width);
                temp.y = r.Next(0, this.height);

            }
        } while (grid.getValue(temp.x, temp.y) != 0 &&
                grid.getValue(temp.x-1, temp.y) != 0 &&
                grid.getValue(temp.x-2, temp.y ) != 0);
        return temp;
    }
    private Vector2Int FindCoordinatesHorizontalWallRightSide(out bool isLeftSide)
    {
        Vector2Int temp = new Vector2Int();
        System.Random r = new System.Random();
        isLeftSide = false;
        do
        {
            int rand = r.Next(0,20);
            if(rand<10)  //Left sid
			{
                temp.x = r.Next(0, this.width/2 - 3);
                temp.y = r.Next(0, this.height);
            }
            else  //Right side
            {
                temp.x = r.Next(this.width / 2 + 3,this.width-3);
                temp.y = r.Next(0, this.height);

            }
        } while (grid.getValue(temp.x, temp.y) != 0 &&
                grid.getValue(temp.x + 1, temp.y) != 0 &&
                grid.getValue(temp.x + 2, temp.y) != 0);
        return temp;
    }

    private void SetUpInBoardWallHorizontal(int x, int y, TileBase horizontalWallTile, bool isLeftSide)
    {
		for (int i = 0; i < 3; i++)
		{
			if (isLeftSide)
			{
                SetWall(x-i, y, horizontalWallTile);
			}
			else
			{
                SetWall(x + i, y, horizontalWallTile);
			}
		}
    }

    public void CreatePlayer()
	{
        Vector3 playerPos = new Vector3(-0.5f, -6.5f, 0);
        playerObj = Instantiate(playerObj, playerPos, playerObj.transform.rotation) as GameObject;
        grid.setValue(10, 1, 2);
        playerObj.GetComponent<Player>().SetGridSystem(this.gameObject.GetComponent<GridMap>());
        playerObj.GetComponent<Player>().SetCoordinates(10, 1);
    }

    public void CreateEnemy(int x, int y)
	{
        Vector3 ePos = new Vector3(-0.5f, 1.5f, 0);
        GameObject g = Instantiate(enemyObj, ePos, enemyObj.transform.rotation) as GameObject;
        enemyObj = g;
        Enemy e = g.GetComponent<Enemy>();
        e.SetXCoordinate(x);
        e.SetYCoordinates(y);
        e.SetPlayer(playerObj.GetComponent<Player>());
        e.SetUpGridSystem(GetGridSystem());
        e.SetUpGridMAp(this.gameObject.GetComponent<GridMap>());
        e.CreateNewaStar();
        grid.setValue(x, y, 3);
	}

	private bool isGameFinished()
	{
        return numOfScores <= 0;
	}

    public void SetBoardInactive()
    {
        tileMap.gameObject.SetActive(false);
        if (enemyObj != null)
            Destroy(enemyObj);
        if(playerObj != null)
            playerObj.SetActive(false);
    }
    public void SetBoardActive()
	{
        this.gameObject.SetActive(true);
        tileMap.gameObject.SetActive(true);
    }
    public void MoveEnemyToCenter()
    {
       enemyObj.transform.position = new Vector3(-0.5f, 1.5f, 0);
       Enemy en = enemyObj.GetComponent<Enemy>();
        en.SetXCoordinate(10);
        en.SetYCoordinates(9);
        grid.setValue(10, 9, 3);
    }
}
