using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<GameObject> enemyList;
    public Queue<GameObject> enemySpawnQueue;
    public List<GameObject> spawnedEnemyList;
    public GameObject playerObject;
    public Player player;
    public float spawnTimer = 2.0f;
    public float attackTime = 1.2f;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnQueue = CreateSpawnQueue(level);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer = UpdateTimer(spawnTimer);
        if(this.spawnTimer<= 0f && enemySpawnQueue.Peek()!= null)
		{
            SpawnEnemy(enemySpawnQueue, 0);
            spawnTimer = 8.1f;
		}
    }

    public Queue<GameObject> CreateSpawnQueue(int level)
	{
        Queue<GameObject> tempQueue = new Queue<GameObject>(20);
		for (int i = 0; i < 8+2*level; i++)
        {
            int rand_num = Random.Range(0, enemyList.Count); //Generates random number.
            tempQueue.Enqueue(enemyList[rand_num]); //Add the enemy to queue.
            Debug.Log("Add check.");
        }
        return tempQueue;
	}

    public bool LevelClearaceCheck()
	{
        bool finished = false;
        if (spawnedEnemyList.Count == 8 + 2 * level && spawnedEnemyList[8 + 2 * level - 1] == null)
		{
            Debug.Log("Finish check.");
            finished = true;
		}
        return finished;
	}

    public void SpawnEnemy(Queue<GameObject> queue, float xrange)
	{
        int rand_num = Random.Range(0, 2);
        GameObject gameObject = queue.Dequeue();
		if (rand_num == 0)
        {
            GameObject createdObject = Instantiate(gameObject, new Vector3(9.1f+playerObject.transform.position.x, -2.43f, 1), gameObject.transform.rotation) as GameObject; //Creates the object.
            createdObject.GetComponent<Enemies>().GetPlayerComponent(player.gameObject); // Gets the player components.
            spawnedEnemyList.Add(createdObject);
        }
		else
        {
            GameObject createdObject = Instantiate(gameObject, new Vector3(playerObject.transform.position.x - 9.75f, -2.43f, 1), gameObject.transform.rotation) as GameObject; //Creates the object.
            createdObject.GetComponent<Enemies>().GetPlayerComponent(player.gameObject); // Gets the player components.
            spawnedEnemyList.Add(createdObject);

        }
        Debug.Log("Spawn check.");
	} 

    public float UpdateTimer(float timer)
	{
        float temp_timer = timer - Time.deltaTime;
        return temp_timer;
	}


}
