using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Tilemap))]
public class GridMap : MonoBehaviour
{
    public int width;
    public int height;
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
	}
    private void SetWall(int width, int height, TileBase tile)
	{
        tileMap.SetTile(new Vector3Int(width, height, 0), tile);
        grid.setValue(width, height, 1); // 1 is being used for walls.
    }
}
