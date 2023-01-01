using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/Gold")]
public class GoldUp : PowerUps
{
	public int amount;
	public override void Apply(GameObject target)
	{
		Player pl = target.GetComponent<Player>();
		if (pl != null)
		{
			pl.gold += amount;
			Debug.Log("old Debug.");
		}
	}
}
