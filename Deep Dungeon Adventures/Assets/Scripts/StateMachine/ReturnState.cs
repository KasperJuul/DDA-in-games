using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : BaseState
{
    private EnemyInput enemy;

    // Constructer
    public ReturnState(EnemyInput _enemy) : base(_enemy.gameObject)
    {
        enemy = _enemy;
    }

    public override Type Tick()
    {
        if (enemy.CheckTargetDistance() < Mathf.Pow(EnemySettings.AggroRadius, 2))
        {
            enemy.destinationSetter.target = enemy.player.transform;
            return typeof(ChaseState);
        }

        if(transform.position == enemy.initialPos)
        {
            enemy.animator.SetBool("IsRunning", false);
            enemy.aIPath.canMove = false;
            return typeof(IdleState);
        }

        return null;
    }

    public override void FixedTick()
    {
        //enemy.Move(enemy.initialPos);
    }
}
