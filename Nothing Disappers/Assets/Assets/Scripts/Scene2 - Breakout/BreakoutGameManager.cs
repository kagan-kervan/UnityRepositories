using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutGameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bricks;
    public SceneManager scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (CheckGameOver())
		{
            Debug.Log("Game End.");
            scene.ActiveTransition();
		}
    }

    private bool CheckGameOver()
	{
		for (int i = 0; i < bricks.GetLength(0); i++)
		{
            if (bricks[i] != null)
                return false;
            
		}
        return true;
	}
}
