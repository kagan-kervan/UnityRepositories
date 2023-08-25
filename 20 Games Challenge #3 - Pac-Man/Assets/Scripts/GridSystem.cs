using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
	private int width;
	private int height;
	private int[,] gridArray;
	private float cellSize;
	public GridSystem(int width, int height, float cellSize)
	{
		this.width = width;
		this.height = height;
		gridArray = new int[width, height];
		this.cellSize = cellSize;
	}

	public int[,] getArray()
	{
		return gridArray;
	}

	public void SetUpNewGrid(int width , int height)
	{
		gridArray = new int[width, height];
		this.width = width;
		this.height = height;
	}
	public void setValue(int x, int y, int value)
	{
		if(x>=0 && y>=0 && x<width && y < height)
		{
			gridArray[x, y] = value;
			Debug.Log("Value setted to " + value +"in "+x+" , "+y);
		}

	}


	public void setValue(Vector3 worldPosition,int value)
	{
		Vector2Int coordinates = GetCoordinates(worldPosition);
		setValue(coordinates.x, coordinates.y,value);
	}

	private Vector2Int GetCoordinates(Vector3 worldPosition)
	{
		Vector2Int temp = new Vector2Int();
		temp.x = Mathf.FloorToInt(worldPosition.x/cellSize);
		temp.y = Mathf.FloorToInt(worldPosition.y / cellSize);
		return temp;
	}

	public int getValue(int x, int y)
	{
		if (x >= 0 && y >= 0 && x < width && y < height)
		{
			return gridArray[x, y];
		}
		return 0;
	}
	public int getValue(Vector3 worldPosition)
	{
		Vector2Int temp = GetCoordinates(worldPosition);
		return getValue(temp.x, temp.y);
	}
}
