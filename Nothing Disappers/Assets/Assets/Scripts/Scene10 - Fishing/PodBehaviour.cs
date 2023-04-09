using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodBehaviour : MonoBehaviour
{
    public GameObject[] fishObjects;
    public SceneManager scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
	{
        Vector2 destVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destVector.x = Mathf.Max(destVector.x, -7.5f);
        destVector.x = Mathf.Min(destVector.x, 7.5f);
        destVector.y = Mathf.Max(destVector.y, -4.25f);
        destVector.y = Mathf.Min(destVector.y, 4.44f);
        this.transform.position = destVector;
        if (EndGameCheck())
        {
            Debug.Log("End game.");
            scene.ActiveTransition();
            //Screen deðiþ.
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        FishBehaviour fish = collision.collider.GetComponent<FishBehaviour>();
        if (fish != null)
        {
            Destroy(collision.collider.gameObject);
        }
		else
		{
            //end gamwe.
            Destroy(gameObject);
            scene.GoLoseMenu();
		}

	}

    private bool EndGameCheck()
	{
		for (int i = 0; i < fishObjects.Length; i++)
		{
            if (fishObjects[i] != null)
                return false;
		}
        return true;
	}
}
