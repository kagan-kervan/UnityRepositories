using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherGameManager : MonoBehaviour
{
    public GameObject arrowObject;
    public GameObject[] targets;
    public GameObject[] hittedTargetsObj;
    public SceneManager scene;
    private float amount = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AimMovement();
        if (Input.GetMouseButtonDown(0))
            FireArrow();
		if (EndGameCheck())
		{
            scene.ActiveTransition();
		}
    }

    private void AimMovement()
	{
        Vector2 destVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destVector.x = Mathf.Max(destVector.x, -7.5f);
        destVector.x = Mathf.Min(destVector.x, 7.5f);
        destVector.y = Mathf.Max(destVector.y, -4.25f);
        destVector.y = Mathf.Min(destVector.y, 4.44f);
        this.transform.position = destVector;
    }
    public void FireArrow()
	{
        amount--;
        Vector2 pos = transform.position;
        GameObject tempArrow = Instantiate(arrowObject, pos, arrowObject.transform.rotation);
        tempArrow.GetComponent<ArcheryArrowBehaviour>().InitializeManager(this.gameObject);
	}

    public void HitTarget(GameObject obj)
	{
        Vector2 pos = obj.transform.position;
        Debug.Log("Enters collision");
		for (int i = 0; i < targets.Length; i++)
		{
            if(targets[i]!= null)
			{
                float left_range = targets[i].transform.position.x - 1.5f;
                float right_range = targets[i].transform.position.x + 1.5f;
                if(pos.x >= left_range && pos.x<=right_range)
				{
                    Destroy(targets[i]);
                    hittedTargetsObj[i].SetActive(true);
				}
			}
		}
	}

    private bool EndGameCheck()
	{
		if (amount < 0)
		{
            scene.GoLoseMenu();
		}
		for (int i = 0; i < targets.Length; i++)
		{
            if (targets[i] != null)
                return false;
		}
        return true;
	}

}
