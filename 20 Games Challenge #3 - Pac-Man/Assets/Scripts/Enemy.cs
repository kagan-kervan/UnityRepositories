using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int x_coordinates;
    private int y_coordinates;
    private GridSystem gridSystem;
    public Player.Direction direction;
    public float y_axisPerUnit;
    public float x_axisPerUnit;


    public void SetXCoordinate(int value)
	{
        x_coordinates = value;
	}
    public void SetYCoordinates(int value)
	{
        y_coordinates = value;
	}

	private void OnEnable()
	{
        spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveEnemy()
	{
        Vector3 pos = this.transform.position;
        switch (direction)
        {
            case Player.Direction.DOWN:
                pos.y = pos.y - y_axisPerUnit;
                break;
            case Player.Direction.UP:
                pos.y = pos.y + y_axisPerUnit;
                break;
            case Player.Direction.RIGHT:
                pos.x = pos.x + x_axisPerUnit;
                break;
            case Player.Direction.LEFT:
                pos.x = pos.x - x_axisPerUnit;
                break;
        }
        this.transform.position = pos;
    }

    public void ChangePlayerSprite()
    {
        switch (direction)
        {
            case Player.Direction.DOWN:
                spriteRenderer.sprite = sprites[0];
                break;
            case Player.Direction.UP:
                spriteRenderer.sprite = sprites[0];
                break;
            case Player.Direction.RIGHT:
                spriteRenderer.sprite = sprites[1];
                break;
            case Player.Direction.LEFT:
                spriteRenderer.sprite = sprites[2];
                break;
        }
    }


}
