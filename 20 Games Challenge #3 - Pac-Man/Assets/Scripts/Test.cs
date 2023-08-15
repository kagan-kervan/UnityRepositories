using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private void Start()
	{
		GridSystem grid = new GridSystem(20, 10,32f);
		 
	}

	public Vector3 GetMousePosition(Vector3 screenPosition, Camera worldCamera)
	{
		Vector3 vec = worldCamera.ScreenToWorldPoint(screenPosition);
		return vec;
	}
}
