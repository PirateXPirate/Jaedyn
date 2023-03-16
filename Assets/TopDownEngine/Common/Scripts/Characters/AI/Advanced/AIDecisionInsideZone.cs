using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionInsideZone : AIDecision
{
	[SerializeField] EnemyZone zoneCollider;
    bool isHit = false;
	public override void Initialization()
	{
		zoneCollider.OnTriggerPlayer += OnPlayerHit;
	}

    private void OnPlayerHit(Transform target)
    {
        isHit = true;
        _brain.Target = target;
    }

    public override bool Decide()
    {
       
        return isHit;
    }
}
