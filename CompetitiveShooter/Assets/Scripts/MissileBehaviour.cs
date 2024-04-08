using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{

    public float movementSpeed;
    public float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveMissile(Time.deltaTime);
        if (!CheckDestructionPos(transform.position))
        {
            Destroy(this.gameObject);
        }
    }


    //Basic missile movement(only on Y axis)
    private void MoveMissile(float delta_time)
    {
        Vector2 pos = transform.position;
        pos.y += delta_time*movementSpeed;
        transform.position = pos;
    }

    public bool OnCollisionEnter2D(Collision2D collision)
    {
        ScoreAsteroidBehaviour scoreAsteroid = collision.collider.GetComponent<ScoreAsteroidBehaviour>();
        if (scoreAsteroid != null)
        {
            scoreAsteroid.TakeHit();
            Destroy(this.gameObject);
            return true;
        }
        ExpAsteroidBehaviour expAsteroid = collision.collider.GetComponent<ExpAsteroidBehaviour>();
        if(expAsteroid != null)
        {
            expAsteroid.TakeHit();
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    private bool CheckDestructionPos(Vector2 pos)
    {
        return pos.y <= offsetY;
    }
}
