using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject bulletObject;
    public float firingDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        firingDelay = UpdateTimer(firingDelay, Time.deltaTime);
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
}
