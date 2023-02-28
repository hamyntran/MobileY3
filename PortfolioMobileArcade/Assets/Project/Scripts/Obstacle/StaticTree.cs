using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*------------------------------------------
Author: NAME
Last modified by: NAME
-------------------------------------------*/
 
/// <summary>
/// 
/// </summary>

public class StaticTree : Obstacle
{
	[SerializeField] private int coinReward = 5;
	
	protected override void Start()
	{
		base.Start();
		Destroyable = new StaticAndCoinDestroyable(coinReward);
	}
	
}