using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum State
	{
        Player1,Player2
	}
    [SerializeField] State players;
    public float speedMultiplier;
    public Rigidbody2D rgb2D;
    [Header("Car Settings")]
    public float driftFactor = 0.85f;
    public float accelerationFactor;
    public float turnFactor = 3.5f;


    //Local Components.
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;


	private void Awake()
	{
        rgb2D = GetComponent<Rigidbody2D>();
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TakeInputs();
    }

	private void FixedUpdate()      //Same As Update but used in physic movement.
	{
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
	}

    public void ApplyEngineForce()
	{
        if (accelerationInput == 0)
            rgb2D.drag = Mathf.Lerp(rgb2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else
            rgb2D.drag = 0;

        Vector2 engineVector = transform.up * accelerationFactor * accelerationInput;

        rgb2D.AddForce(engineVector, ForceMode2D.Force);
	}

    public void ApplySteering()
	{
        float minSpeed = (rgb2D.velocity.magnitude / 8);
        minSpeed = Mathf.Clamp01(minSpeed);
        rotationAngle -= steeringInput * turnFactor*minSpeed;


        rgb2D.MoveRotation(rotationAngle);
	}

    private void KillOrthogonalVelocity()
	{
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rgb2D.velocity, transform.up);
        Vector2 rightVelcoity = transform.right * Vector2.Dot(rgb2D.velocity, transform.right);

        rgb2D.velocity = forwardVelocity + rightVelcoity * driftFactor;
	}

    public void TakeInputs()
	{
        Vector2 inputVectors = Vector2.zero;
        if(players == State.Player1)
        {
            inputVectors.x = Input.GetAxis("HorizontalMovement");
            inputVectors.y = Input.GetAxis("VerticalMovement");
        }
        else if( players == State.Player2)
		{
            inputVectors.x = Input.GetAxis("HorizontalMovementPlayer2");
            inputVectors.y = Input.GetAxis("VerticalMovementPlayer2");
        }
        SetUpInputs(inputVectors);
	}

    public void SetUpInputs(Vector2 inputVec)
	{

        steeringInput = inputVec.x;
        accelerationInput = inputVec.y;
	}

    public string GetState()
	{
        if (players == State.Player1)
            return "PLAYER1";
        else
            return "PLAYER2";
	}

}
