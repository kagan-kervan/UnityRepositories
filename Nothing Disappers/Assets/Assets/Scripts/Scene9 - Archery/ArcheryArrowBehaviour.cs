using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryArrowBehaviour : MonoBehaviour
{
    public ArcherGameManager manager;
    private float deleteDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update()
    {
        
    }

    public void InitializeManager(GameObject obj)
	{
        manager = obj.GetComponent<ArcherGameManager>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Obstacle")
		{
            manager.HitTarget(collision.collider.gameObject);
		}
	}
}
