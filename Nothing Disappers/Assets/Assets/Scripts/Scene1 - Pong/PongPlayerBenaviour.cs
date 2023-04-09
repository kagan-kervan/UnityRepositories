using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerBenaviour : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    private Rigidbody2D rgb;
    // Start is called before the first frame update
    void Start()
    {
        rgb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
	{
        Vector2 destinationVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destinationVector.y = transform.position.y; // Keep y same.
        // Keeping the paddle in the screen.
        destinationVector.x = Mathf.Max(destinationVector.x, -7.5f);
        destinationVector.x = Mathf.Min(destinationVector.x, 7.5f);
        this.transform.position = destinationVector;
	}
}
