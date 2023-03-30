using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speedMultiplier;
    public GameObject firedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement(Time.deltaTime);
    }

    public void Movement(float time)
	{
        Vector3 translatePos;
        if (firedObject.GetComponent<EnemyBehaviour>()!= null)
		{
           translatePos = new Vector3(0.0f, -speedMultiplier, 0.0f);
        }
		else
        {
           translatePos = new Vector3(0.0f, speedMultiplier, 0.0f);

        }
        this.transform.Translate(translatePos*time);
        if (CheckforDistance())
            Destroy(gameObject);
	}
    public void InstantitePlayerObject(GameObject playerObj)
	{
        firedObject = playerObj;
	}
    public bool CheckforDistance()
	{
        return Mathf.Abs(firedObject.transform.position.z - this.transform.position.z) > 90;
	}

	public void OnCollisionEnter(Collision collision)
	{
        Debug.Log("Collision enter.");
	}
}
