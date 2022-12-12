using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemies
{
    public int health;
    public int hitPoint;
    public float speedMultiplier;
    public Animator animator;
    public Player player;
    public GameObject playerObject;
    public Rigidbody2D rgb2D;
    public Transform AttackPoint;
    private float hitTimer = 0f;
    private float deadTimer = 1.5f;
    public bool isFaceRight = true;
    public float distance;
    public Missile missileBehaviour;
    private void OnEnable()
    {
        enemyStates = States.MOVING;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetPlayerComponent(GameObject obj)  //Use it when you create this objects in game mangaer.
    {
        player = obj.GetComponent<Player>();
        playerObject = player.gameObject;
	}
    // Update is called once per frame
    void Update()
    {
        distance = UpdateDistance();
        hitTimer = UpdatetheTimer(hitTimer);
        if (enemyStates != States.DEATH)
            Movement();
		else
		{
            deadTimer = UpdatetheTimer(deadTimer);
            if (deadTimer <= 0)
                Destroy(gameObject);
        }
        if (enemyStates==States.ATTACK) // in the distance and able to attack.
        {
            Attack();
        }
    }
    public float UpdatetheTimer(float timer)
    {
        timer = timer - Time.deltaTime;
        return timer;
    }
    public void FliptheObject(bool isFaceRight)
    {
        if (distance < 0.5f)
            return;
        Quaternion rotate = this.transform.rotation;
        if (isFaceRight == true) // if right is where to face.
        {
            rotate.y = 0;
            this.transform.rotation = rotate;
            missileBehaviour.isFiringRight = true;  //Change missile's direction.
        }
        else  // if left is where to face.
        {
            rotate.y = 180;
            this.transform.rotation = rotate;
            missileBehaviour.isFiringRight = false;  //Change missile's direction.

        }
    }
    public override void Attack()
	{
		if(hitTimer <= 0)
		{
            Vector3 missilePos = transform.position;
            if (missileBehaviour.isFiringRight)
                missilePos.x += 1;
            else
                missilePos.x += -1;
            missilePos.y += -0.6f;
            animator.SetTrigger("ShotTrigger");
            Instantiate(missileBehaviour.gameObject, missilePos, transform.rotation);
            hitTimer = 2.5f; //Reset hit timer.
		}
	}

    public float UpdateDistance()
    {
        Vector3 objectPosiiton = this.transform.position;
        Vector3 targetPosition = playerObject.transform.position;
        distance = Mathf.Abs(objectPosiiton.x - targetPosition.x);
        return distance;
    }

	public override void Movement()
    {
        Vector3 objectPosiiton = this.transform.position;
        Vector3 targetPosition = playerObject.transform.position;
        if (distance > 4.0f)
        {
            enemyStates = States.MOVING;
            float newVelocity_x = Mathf.Pow(distance / 3, speedMultiplier);
            if (targetPosition.x < objectPosiiton.x)
                newVelocity_x = -newVelocity_x;
            Vector3 velocity = rgb2D.velocity;
            velocity.x = newVelocity_x;
            rgb2D.velocity = velocity;
            animator.SetBool("MovementBool", true);
        }
        else
        {
            rgb2D.velocity = new Vector2(0, 0);
            animator.SetBool("MovementBool", false);
            enemyStates = States.ATTACK;
        }
        if (objectPosiiton.x > targetPosition.x) //Face Right
            isFaceRight = false;
        else //Face Left
            isFaceRight = true;
        FliptheObject(isFaceRight);
    }

	public override void TakeHit()
    {
        this.health -= 20;
        animator.SetTrigger("TakeHit");
        if (this.health <= 0)
        {
            enemyStates = States.DEATH;
            rgb2D.velocity = new Vector2(0, 0);
            animator.SetTrigger("DeadTrigger");
        }
    }
}
