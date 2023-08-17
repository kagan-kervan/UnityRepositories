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

    // Start is called before the first frame update
    void Start()
    {
        grid = new GridSystem(width, height, 16f);
        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void SetUpTheCenterArea(int midPointhorizontal, int midPointVertical, TileBase tile)
	{
		for (int i = 0; i < 4; i++)
		{
            //Sets up the other half.
            tileMap.SetTile(new Vector3Int(midPointhorizontal-2,midPointVertical-i,0),tile);
            tileMap.SetTile(new Vector3Int(midPointhorizontal + 2, midPointVertical - i, 0), tile);
        }
		for (int i = 0; i < 2; i++)
		{
            tileMap.SetTile(new Vector3Int(midPointhorizontal + i, midPointVertical, 0), tile); 
            tileMap.SetTile(new Vector3Int(midPointhorizontal - i, midPointVertical, 0), tile);
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

}