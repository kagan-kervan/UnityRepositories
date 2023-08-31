using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using UnityEngine;

public class Node
{
	public Node parent;
	public Vector2Int position;
	public float distance;
	public bool walkable;
    public Node(Vector2Int pos, bool walkable)
    {
		parent = null;
		position = pos;
		distance = -1;
		this.walkable = walkable;
    }
}
public class AStar : MonoBehaviour
{
	private int[,] grid;
	public AStar(int[,] gridArray)
	{
		grid = gridArray;
	}

	public Stack<Node> FindPath(Vector2Int start, Vector2Int end)
	{
		Node startNode = new Node(start, true);
		Node endNode = new Node(end, true);
		Stack<Node> stack = new Stack<Node>();
		List<Node> openList =  new List<Node>();
		List<Node> ClosedList = new List<Node>();
		List<Node> adjacencies;
		Node current = startNode;
		openList.Add(startNode);
		while(openList.Count!= 0 && !ClosedList.Exists(x => x.position == endNode.position))
		{
			AddToList(openList,current);
			ClosedList.Add(current);
			adjacencies = GetAdjacentNodes(current);
			foreach(Node n in adjacencies)
			{
				if(!ClosedList.Contains(n) && n.walkable)
				{
					bool isFound = false;
					foreach (var oLNode in openList)
					{
						if (oLNode == n)
						{
							isFound = true;
						}
					}
					if (!isFound)
					{
						n.parent = current;
						n.distance = Math.Abs(n.position.x - endNode.position.x) + Math.Abs(n.position.y - endNode.position.y);
						AddToList(openList, n);
					}
				}
			}
		}
		// construct path, if end was not closed return null
		if (!ClosedList.Exists(x => x.position == endNode.position))
		{
			return null;
		}

		// if all good, return path
		Node temp = ClosedList[ClosedList.IndexOf(current)];
		if (temp == null) return null;
		do
		{
			stack.Push(temp);
			temp = temp.parent;
		} while (temp != startNode && temp != null);
		return stack;
	
}

	private void AddToList(List<Node> openList, Node data)
	{
		int i = 0;
		while (data.distance > openList.ElementAt(i).distance)
		{
			i++;
		}
		openList.Insert(i, data);
	}

	private List<Node> GetAdjacentNodes(Node current)
	{

		List<Node> temp = new List<Node>();
		int row = (int)current.position.y;
		int col = (int)current.position.x;
		if(isValid(col,row+1) && isUnblockable(col,row+1))
		{
			Node tempN = new Node(new Vector2Int(col, row + 1), true);
			temp.Add(tempN);
		}
		if(isValid(col,row-1) && isUnblockable(col, row - 1))
		{

			Node tempN = new Node(new Vector2Int(col, row - 1), true);
			temp.Add(tempN);
		}
		if(isValid(col+1,row) && isUnblockable(col + 1, row))
		{
			Node tempN = new Node(new Vector2Int(col+1, row), true);
			temp.Add(tempN);
		}

		if (isValid(col - 1, row) && isUnblockable(col - 1, row))
		{
			Node tempN = new Node(new Vector2Int(col - 1, row), true);
			temp.Add(tempN);
		}
		return temp;
	}

	private bool isUnblockable(int coord_x, int coord_y)
	{
		if (grid[coord_x, coord_y] != 0 || grid[coord_x, coord_y] != 3)
			return false;
		return true;
	}

	private bool isValid(int x, int y)
	{
		if (x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1))
		{
			return true;
		}
		return false;
	}

	private void SortLists(List<Node> l)
	{

	}
}
