using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAsteroidBehaviour : MonoBehaviour
{

    public float life;
    [SerializeField]
    private Rigidbody2D objectBody;
    [SerializeField]
    private Vector2 forceVector;
    public float spanwOffSetRight;
    public float spawnOffSetLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAlivability();
    }

    bool IsAlive()
    {
        return life > 0;
    }

    public void TakeHit()
    {
        life = life - 1;
        if (IsAlive())
        {
            objectBody.AddForce(forceVector);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>() != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void CheckForAlivability()
    {
        if (this.transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }
    }
}
