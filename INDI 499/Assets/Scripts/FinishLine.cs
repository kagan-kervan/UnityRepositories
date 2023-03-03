using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
	public TextMeshProUGUI gameOverText;
	public SceneManager manager;
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Player pl = collision.collider.GetComponent<Player>();
		if (pl != null)
		{
			Debug.Log("Finishhh!!!!!!!!!");
			gameOverText.text = pl.GetState() + " WINS";
			manager.ActivateGameOverMenu();
		}
	}

}
