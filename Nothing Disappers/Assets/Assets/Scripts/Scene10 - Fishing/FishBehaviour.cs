using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    [SerializeField] private bool isHeadingLeft = false;
    [SerializeField] private float speedMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

	private void Move()
	{
        if (isHeadingLeft)
        {
            Vector2 new_pos = transform.position;
            new_pos.x = new_pos.x - speedMultiplier * Time.deltaTime;
            this.transform.position = new_pos;
        }
        else
        {
            Vector2 new_pos = transform.position;
            new_pos.x = new_pos.x + speedMultiplier * Time.deltaTime;
            this.transform.position = new_pos;
        }
        CheckForRotation();
    }

    private void CheckForRotation()
    {
        if (this.transform.position.x >= 7.5f)
        {
            isHeadingLeft = true;
            Flip();
        }
        else if (this.transform.position.x <= -7.5f)
        {
            isHeadingLeft = false;
            Flip();
        }
    }

    private void Flip()
	{
        Quaternion rotate = this.transform.rotation;
        if (rotate.z > 0)
            rotate.z = 0;
        else
            rotate.z = 180;
        this.transform.rotation = rotate;
	}
}
