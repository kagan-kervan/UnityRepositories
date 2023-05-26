using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    public float health = 5f;
    public GameObject playerObject;
    public GameObject bulletObject;
    public float speedMultiplier;
    public float firingDelay;
    public Rigidbody rgb;
    public float xAxisNegativeOffset;
    public float xAxisPozitiveOffset;
    public float yAxisNegativeOffset;
    public float yAxisPositiveOffset;
    public NavMeshAgent navMesh;
    private Vector3[] positions;
    private int destPoint;
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[3];
        firingDelay = 2.5f;
        rgb = this.GetComponent<Rigidbody>();
        navMesh = this.GetComponent<NavMeshAgent>();
        destPoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        firingDelay = UpdateTimer(firingDelay, Time.deltaTime);
		if (firingDelay <= 0)
		{
            firingDelay = 2.5f;
            Fire();
		}
        if(!navMesh.pathPending)
		{
            GotoNextPoint();
		}
    }
	private void FixedUpdate()
	{
        
	}

	public void InstantitePlayerObject(GameObject playerObj)
    {
        playerObject = playerObj;
    }
    public float CheckforDistance()
    {
        return this.transform.position.z - playerObject.transform.position.z;
    }
    public float  UpdateTimer(float time, float deltaTime)
	{
        time = time - deltaTime;
        return time;
	}
    public void Fire()
	{
        //Creates new bullet position.
        Vector3 pos = new Vector3(this.gameObject.transform.position.x - 0.92f, (this.gameObject.transform.position.y + 0.1f), this.gameObject.transform.position.z + 1f);
        //Creates the bullet.
        GameObject tempBullet = Instantiate(bulletObject, pos, bulletObject.transform.rotation);
        //Attaches player object.
        tempBullet.GetComponent<BulletBehaviour>().InstantitePlayerObject(gameObject);
    }

    public void EnemyMovement()
	{

	}

    public void GotoNextPoint()
	{
        if(positions[0] == null)
		{
            CreatePatrolPositions();
		}
        navMesh.destination = positions[destPoint];
        destPoint = (destPoint+1) % positions.Length;
	}

    private void CreatePatrolPositions()
	{
        positions[0] = this.transform.position;
		for (int i = 1; i < positions.Length; i++)
		{
            float x_axis = Random.Range(-10.2f, 10.2f);
            float y_axis = Random.Range(-5.6f, 5.6f);
            float x_pos = Mathf.Min(xAxisPozitiveOffset, transform.position.x + x_axis);
            x_pos = Mathf.Max(xAxisNegativeOffset, transform.position.x + x_axis);
            float y_pos = Mathf.Min(yAxisPositiveOffset, transform.position.y + y_axis);
            y_pos = Mathf.Max(yAxisNegativeOffset, transform.position.x + y_axis);
            Vector3 pos = new Vector3(x_pos, y_pos, transform.position.z);
            positions[i] = pos;
		}
	}

    public void TakeDamage()
	{
        this.health--;
		if (health < 0)
		{
            // yok et.
            Destroy(gameObject);
		}
	}
}
