using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    public SceneManager sceneManager;
    public List<GameObject> enemyList;
    public Queue<GameObject> enemySpawnQueue;
    public List<GameObject> spawnedEnemyList;
    public GameObject playerObject;
    public Player player;
    public float spawnTimer = 2.0f;
    public float attackTime = 1.2f;
    public int level;
    public bool spawner = true;
    public bool created = false;
    public int spawnCount;
    public int totalEnemyCount;
    public GameObject highScoreObject;

    private void Awake()
    {
        Screen.SetResolution(1366, 768, true);
        if (instance)
            Destroy(gameObject);
        else
        {
            instance = this;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        enemySpawnQueue = CreateSpawnQueue(level);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer = UpdateTimer(spawnTimer);
        if (LevelClearaceCheck())
            FinishtheLevel();
        if (this.spawnTimer<= 0f && spawner)
		{
            SpawnEnemy(enemySpawnQueue, 0);
            spawnTimer = 5.1f;
		}
		if (player.isDead())
		{
            player.ActivateDeathState();
            sceneManager.ActivateGameOverMenu();
            created = false;
		}
    }

    public Queue<GameObject> CreateSpawnQueue(int level)
	{
        totalEnemyCount = 0;
        Queue<GameObject> tempQueue = new Queue<GameObject>(20);
		for (int i = 0; i < 8+2*level; i++)
        {
            int rand_num = Random.Range(0, enemyList.Count); //Generates random number.
            tempQueue.Enqueue(enemyList[rand_num]); //Add the enemy to queue.
            spawnCount++;
            totalEnemyCount++;
        }
        created = true;
        SpawnEnemy(tempQueue, 0);
        spawner = true;
        return tempQueue;
	}

    public bool LevelClearaceCheck()
	{
        bool finished = false;
        if (this.spawnedEnemyList.Count == totalEnemyCount && isListClear(spawnedEnemyList) && created)
		{
            Debug.Log("Finish check.");
            finished = true;
		}
        return finished;
	}

    public void FinishtheLevel()
	{
        Debug.Log("Finish level debug.");
        playerObject.GetComponent<Player>().isAbletoMove = false;
        player.playerStates = Player.States.INMENU;
        spawnedEnemyList.Clear();
        enemySpawnQueue.Clear();
        sceneManager.UpdateLevelText(level);
        sceneManager.SetObjectActive(sceneManager.levelClearenceObject);
	}
    public bool isListClear(List<GameObject> givenList)
	{
        int count = givenList.Count;
		for (int i = 0; i < count; i++)
		{
            if (givenList[i] != null)
                return false;
		}
        return true;
	}

    public void CreateNewLevel()
    {
        level++;
        sceneManager.BackgroundRandomizer();
        playerObject.SetActive(true);
        playerObject.GetComponent<Player>().ResetPositions();
        enemySpawnQueue = CreateSpawnQueue(level);
        sceneManager.SetObjectNotActive(sceneManager.menuObject);
        sceneManager.SetObjectActive(sceneManager.levelMusicObject);
    }

    public void SpawnEnemy(Queue<GameObject> queue, float xrange)
    {
        if (!created)
            return;
        int rand_num = Random.Range(0, 2);
        GameObject gameObject = queue.Dequeue();
		if (rand_num == 0 && gameObject!= null)
        {
            GameObject createdObject = Instantiate(gameObject, new Vector3(9.1f+playerObject.transform.position.x, -2.43f, 1), gameObject.transform.rotation) as GameObject; //Creates the object.
            createdObject.GetComponent<Enemies>().GetPlayerComponent(player.gameObject); // Gets the player components.
            spawnedEnemyList.Add(createdObject);
            spawnCount --;
        }
		else if(gameObject != null)
        {
            GameObject createdObject = Instantiate(gameObject, new Vector3(playerObject.transform.position.x - 9.75f, -2.43f, 1), gameObject.transform.rotation) as GameObject; //Creates the object.
            createdObject.GetComponent<Enemies>().GetPlayerComponent(player.gameObject); // Gets the player components.
            spawnedEnemyList.Add(createdObject);
            spawnCount--;
        }
        Debug.Log("Spawn check.");
        if (spawnCount == 0)
            spawner = false;
	} 

    public void RestartGame()
	{
        level = 1;
        player.ResetAblities();
        ClearTheLevel();
        player.animator.SetBool("DeadBool", false);
        sceneManager.SetObjectNotActive(sceneManager.gameOverObject);
        sceneManager.SetObjectActive(sceneManager.healthText.gameObject);
        sceneManager.ResetButtons();
        sceneManager.ActivateMenu();
	}

    public void ClearTheLevel()
	{
		for (int i = 0; i < spawnedEnemyList.Count; i++)
		{
            GameObject obj = spawnedEnemyList[i];
            if(obj!= null)
			{
                Destroy(obj);
			}
		}
        spawnedEnemyList.Clear();
        enemySpawnQueue.Clear();
	}
    public void OpenHighScoreTable(string s)
    {
        ClearTheLevel();
        sceneManager.DeactivateGameOverMenu();
        this.GetComponent<HighScore>().AddPlayertoHighScore((int)player.gold * 4 * level + 200, s);
        this.GetComponent<HighScore>().CreateList();
        highScoreObject.SetActive(true);
	}
    public void GotoMenu()
	{
        highScoreObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
    public float UpdateTimer(float timer)
	{
        float temp_timer = timer - Time.deltaTime;
        return temp_timer;
	}


}
