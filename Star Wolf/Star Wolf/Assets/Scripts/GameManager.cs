using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerBehaviour player;
    public BulletBehaviour bullet;
    public float firingDelay = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckforInputs();
        firingDelay = UpdateTime(firingDelay, Time.deltaTime);
    }
    public void InstantiateBullet()
    {
        //Creates new bullet position.
        Vector3 pos = new Vector3(player.gameObject.transform.position.x - 0.92f, player.gameObject.transform.position.y+0.1f, player.gameObject.transform.position.z - 1f);
        //Creates the bullet.
        GameObject tempBullet = Instantiate(bullet.gameObject, pos, bullet.transform.rotation);
        //Attaches player object.
        tempBullet.GetComponent<BulletBehaviour>().InstantitePlayerObject(player.gameObject);
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
}
