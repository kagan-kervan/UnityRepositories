using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemies
{

    public int health;
    public int hitPoint;
    public float speedMultiplier;
    public float distance;
    public Animator animator;
    public Rigidbody2D rgb2D;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public int hitCount;
    private float hitTimer = 3f;
    private float deadTimer = 2.0f;
    public bool isFaceRight = true;
    public LayerMask playerLayers;
    public List<GameObject> powerUpList;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        enemyStates = States.MOVING;
    }

    // Update is called once per frame
    void Update()
    {
        hitTimer = UpdatetheTimer(hitTimer);
        if (enemyStates == States.DEATH)
            deadTimer = UpdatetheTimer(deadTimer);
        if (enemyStates == States.MOVING)
        {
            Movement();
        }
        else if (enemyStates == States.ATTACK)
        {
            Attack();
        }
        else if (enemyStates == States.DEATH && deadTimer <= 0)
            Destroy(gameObject);
    }

    public float UpdatetheTimer(float timer)
    {
        timer = timer - Time.deltaTime;
        return timer;
    }
    public override void Movement()
    {
        Vector3 objectPosiiton = this.transform.position;
        Vector3 targetPosition = playerObject.transform.position;
        distance = UpdateDistance();
        if (distance < 1.1f)
        {
            rgb2D.velocity = new Vector2(0, 0);
            animator.SetBool("MovementBool", false);
            if (hitTimer <= 0)
            {
                enemyStates = States.ATTACK;
                hitCount = 1;
                hitTimer = 2;
            }
        }
        else
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
        if (objectPosiiton.x > targetPosition.x) //Face Right
            isFaceRight = false;
        else //Face Left
            isFaceRight = true;
        FliptheObject(isFaceRight);
    }
    public float UpdateDistance()
    {
        Vector3 objectPosiiton = this.transform.position;
        Vector3 targetPosition = playerObject.transform.position;
        distance = Mathf.Abs(objectPosiiton.x - targetPosition.x);
        return distance;
    }

    public override void TakeHit(int hitPoint)
    {
        this.health -= hitPoint;
        animator.SetTrigger("TakeHit");
        this.hitTimer = 1.5f;
        if (this.health <= 0)
        {
            enemyStates = States.DEATH;
            GameObject powerUp = PowerUpDrop(powerUpList);
            if (powerUp != null)
            {
                Instantiate(powerUp, new Vector3(this.transform.position.x, -3.7f, 1f), this.transform.rotation);
            }
            rgb2D.velocity = new Vector2(0, 0);
            animator.SetTrigger("DeadTrigger");
        }

    }
    public override void Attack()
    {
        //Detects the player in the attack range.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, playerLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (hitTimer > 0 && hitCount > 0)
            {
                enemy.GetComponent<Player>().TakeHit();
                animator.SetTrigger("CombatTrigger");
                hitCount--;
            }

        }
        enemyStates = States.MOVING;
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
        }
        else  // if left is where to face.
        {
            rotate.y = 180;
            this.transform.rotation = rotate;

        }
    }
    public void OnDrawGizmosSelected()
    { //Draws attack range when the object is selected.
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
