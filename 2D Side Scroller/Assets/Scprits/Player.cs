using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int life;
    public float score;
    public Rigidbody2D rbg2D;
    public float speedMultiplier;
    public Animator animator;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public bool isFaceRight = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float value = GetMovementValue();
            MovePlayer(value);
        if (value==0)
            animator.SetBool("MovementBool", false);
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerCombat();
    }

    public void SpawnPlayer(float startposition_x, float startposition_y)
	{
        Vector3 startposition = new Vector3(startposition_x, startposition_y, 1);
        Instantiate(this.gameObject, startposition, this.transform.rotation);
	}

    public float GetMovementValue()
	{
        float result; 
        //Gets input axis.
        result = Input.GetAxisRaw("Horizontal");
        return result;
	}

    public void MovePlayer(float value)
	{
        //Change the velocity.
        Vector2 newVelocity = rbg2D.velocity;
        newVelocity.x = value * speedMultiplier;
        rbg2D.velocity = newVelocity;
        //Flip the object.
        if (value < 0)
            isFaceRight = false;
        else if(value>0)
            isFaceRight = true;
        FlipTheObject(isFaceRight);
        //Make the animation start.
        animator.SetBool("MovementBool",true);

	}

    public void PlayerCombat()
	{
        //Taking the velocity and making it 0.
        Vector2 velocity = rbg2D.velocity;
        velocity.x = 0;
        rbg2D.velocity = velocity;
        animator.SetTrigger("CombatTrigger");
        //Detects the enemies in the attack range.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
		{
            if (enemy.GetComponent<Enemies>().enemyStates!=Enemy1.States.DEATH)
                enemy.GetComponent<Enemies>().TakeHit();
		}


    }

    public void FlipTheObject(bool isFaceRight)
	{
        Quaternion rotate = this.transform.rotation;
		if (isFaceRight)
		{
            rotate.y = 0;
            this.transform.rotation = rotate;
        }
		else
		{
            rotate.y = 180;
            this.transform.rotation = rotate;
		}
	}

    public void TakeHit()
	{
        this.life--;
        animator.SetTrigger("HitTrigger");
        if (this.life <= 0)
            Debug.Log("Dead Check.");
	}

    
	
    public void OnDrawGizmosSelected()
	{ //Draws attack range when the object is selected.
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
	}
}
