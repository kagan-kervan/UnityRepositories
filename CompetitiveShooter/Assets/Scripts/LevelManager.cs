using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float gameDuration;
    public int player1Score;
    public int player2Score;
    public float scoreAsteroidSpawnTimer;
    public float expAsteroidSpawnTimer;
    public float expAsteroidXPAmount;
    public List<Vector2> spawnPositionsLeft;
    public List<Vector2> spawnPositionsRight;
    public GameObject scoreAsteroidObject;
    public GameObject expAsteroidObject;
    private float initialScoreTimer;
    private float initialXPTimer;

    // Start is called before the first frame update
    void Start()
    {
        initialScoreTimer = scoreAsteroidSpawnTimer;
        initialXPTimer = expAsteroidSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        gameDuration = UpdateTimer(gameDuration,Time.deltaTime);
        if(gameDuration <= 0)
        {
            Debug.Log("END GAME");
        }
        SpeedUpTheScoreAsteroid();
        SpeedUpTheExpAsteroid();
        scoreAsteroidSpawnTimer = UpdateTimer(scoreAsteroidSpawnTimer, Time.deltaTime);
        if(scoreAsteroidSpawnTimer <= 0)
        {
            SpawnScoreObject();
            scoreAsteroidSpawnTimer = initialScoreTimer;
        }
        expAsteroidSpawnTimer = UpdateTimer(expAsteroidSpawnTimer,Time.deltaTime);
        if(expAsteroidSpawnTimer <= 0)
        {
            SpawnExpObject();
            expAsteroidSpawnTimer = initialXPTimer;
        }
    }

    float UpdateTimer(float time,float delta)
    {
        return time - delta;
    }

    void SpeedUpTheScoreAsteroid()
    {
        if(gameDuration <= 20)
        {
            initialScoreTimer = 0.4f;
        }
        else if(gameDuration <= 40)
        {
            initialScoreTimer = 0.6f;
        }
        else if(gameDuration <= 60)
        {
            initialScoreTimer = 0.8f;
        }
        else if(gameDuration <= 80)
        {
            initialScoreTimer = 1.2f;
        }
    }

    void SpeedUpTheExpAsteroid()
    {
        if(gameDuration <= 20)
        {
            initialXPTimer = float.MaxValue;
        }
        else if(gameDuration <= 50)
        {
            initialXPTimer = 0.75f;
        }
        else if(gameDuration <= 80)
        {
            initialXPTimer = 0.5f;
        }
    }

    void SpawnScoreObject()
    {
        //Randomize and instantiate for both sides.
        System.Random rn = new System.Random();
        int rand = rn.Next(0, spawnPositionsLeft.Count);//Pattern'e geçme olacak.
        Instantiate(scoreAsteroidObject, spawnPositionsLeft[rand], scoreAsteroidObject.transform.rotation);
        Instantiate(scoreAsteroidObject, spawnPositionsRight[rand], scoreAsteroidObject.transform.rotation);
    }
    void SpawnExpObject()
    {
        System.Random rn = new System.Random();
        if(initialXPTimer <= 0.5f)
        {
            int objCount = rn.Next(2, 3);
            for(int i = 0; i < objCount; i++)
            {
                int posIndex = rn.Next(0,spawnPositionsLeft.Count);//Pattern'e geçme olacak.
                Instantiate(expAsteroidObject, spawnPositionsLeft[posIndex], expAsteroidObject.transform.rotation);
                Instantiate(expAsteroidObject, spawnPositionsRight[posIndex], expAsteroidObject.transform.rotation);
            }
        }
        else if(initialXPTimer <= 0.75f)
        {
            int objCount = rn.Next(1, 2);
            for (int i = 0; i < objCount; i++)
            {
                int posIndex = rn.Next(0, spawnPositionsLeft.Count); //Pattern'e geçme olacak.
                Instantiate(expAsteroidObject, spawnPositionsLeft[posIndex], expAsteroidObject.transform.rotation);
                Instantiate(expAsteroidObject, spawnPositionsRight[posIndex], expAsteroidObject.transform.rotation);
            }
        }
    }
}
