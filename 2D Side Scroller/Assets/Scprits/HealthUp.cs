using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/HealthUp")]
public class HealthUp : PowerUps
{
	public override void Apply(GameObject target)
	{
		target.GetComponent<Player>().life++;
		Debug.Log("Powerup debug");
	}

}
