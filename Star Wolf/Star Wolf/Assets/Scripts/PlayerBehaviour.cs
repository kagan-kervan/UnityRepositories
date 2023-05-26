using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody rgb;
    public float speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = getHorizontalInput();
        float verticalInput = getVerticalInput();
        Movement(horizontalInput, verticalInput,Time.deltaTime);
    }

    private float getHorizontalInput()
	{
        return Input.GetAxisRaw("Horizontal");
	}
    private float getVerticalInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    private void Movement(float horizontalValue,float verticalValue, float time)
	{
        Vector3 forceVector = new Vector3(horizontalValue * speedMultiplier , verticalValue * speedMultiplier , 0);
        if(isInsidetheCoordinate(horizontalValue * speedMultiplier*time, verticalValue * speedMultiplier*time))
            this.transform.Translate(forceVector * time);
	}
    private bool isInsidetheCoordinate(float horizontalMovementValue, float verticalMovementValue)
	{
        if (Mathf.Abs(this.transform.position.x+horizontalMovementValue) >= 38)
            return false;
        else if (Mathf.Abs(this.transform.position.y+verticalMovementValue) >= 29)
            return false;
        return true;
	}

   
}
