using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerBehaviour player;
    public BulletBehaviour bullet;
    public GameObject[] enemyObjects;
    public float firingDelay = 0.3f;
    public float spawnTimer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckforInputs();
        firingDelay = UpdateTime(firingDelay, Time.deltaTime);
        spawnTimer = UpdateTime(spawnTimer, Time.deltaTime);
		if (spawnTimer <= 0)
		{
            int random = Random.Range(0, 2);
            spawnTimer = 5.0f;
            EnemySpawner(enemyObjects[random]);
        }
    }
    public void InstantiateBullet()
    {
        //Creates new bullet position.
        Vector3 pos = new Vector3(player.gameObject.transform.position.x - 0.92f, player.gameObject.transform.position.y+0.1f, player.gameObject.transform.position.z - 1f);
        //Creates the bullet.
        GameObject tempBullet = Instantiate(bullet.gameObject, pos, bullet.transform.rotation);
        //Attaches player object.
        tempBullet.GetComponent<BulletBehaviour>().InstantitePlayerObject(player.gameObject);
        //Adjusts x-axis for second bullet
        pos.x = player.gameObject.transform.position.x + 0.9f;
        GameObject tempBullet2 = Instantiate(bullet.gameObject, pos, bullet.transform.rotation);
        //Attaches player object.
        tempBullet2.GetComponent<BulletBehaviour>().InstantitePlayerObject(player.gameObject);
        
    }
    public void CheckforInputs()
	{
        // Left click of mouse fires a bullet.
		if (Input.GetMouseButton(0) && firingDelay<= 0)
		{
            InstantiateBullet();
            firingDelay = 0.2f;
		}
	}

    public float UpdateTime(float time, float deltaTime)
	{
        time = time - deltaTime;
        return time;
	}

    public void EnemySpawner(GameObject obj)
    {
        float x_axis = Random.Range(-36.0f, 36.0f);
        float y_axis = Random.Range(-25.0f, 25.0f);
        float z_axis = player.gameObject.transform.position.z + 150;
        Vector3 spawnPos = new Vector3(x_axis, y_axis, z_axis);
        GameObject enemyObj = Instantiate(obj, spawnPos, obj.transform.rotation);
        enemyObj.GetComponent<EnemyBehaviour>().InstantitePlayerObject(player.gameObject);
	}
        
}
