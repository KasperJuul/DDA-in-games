using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private EnemyInput enemy;

    // Constructer
    public IdleState(EnemyInput _enemy) : base(_enemy.gameObject)
    {
        enemy = _enemy;
    }

    public override Type Tick()
    {
        if (enemy.CheckTargetDistance() < Mathf.Pow(EnemySettings.AggroRadius, 2))
        {
            enemy.animator.SetBool("IsRunning", true);
            enemy.destinationSetter.target = enemy.player.transform;
            enemy.aIPath.canSearch = true;
            enemy.aIPath.canMove = true;
            return typeof(ChaseState);
        }

        return null;
    }

}
