using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private EnemyInput enemy;
    float attackTimer = 0.0f;
    bool attackTimerSet = false;

    // Constructer
    public ChaseState(EnemyInput _enemy) : base(_enemy.gameObject)
    {
        enemy = _enemy;
    }

    public override Type Tick()
    {
        if (enemy.player != null)
        {
            Vector3 flipVector = enemy.player.transform.position - enemy.transform.position;
            if (flipVector.x < 0)
            {
                enemy.transform.localScale = new Vector3(-1f, 1, 1);
            }
            else if (flipVector.x > 0)
            {
                enemy.transform.localScale = new Vector3(1f, 1, 1);
            }
        }

        if (enemy.CheckTargetDistance() > Mathf.Pow(EnemySettings.AggroRadius, 2))
        {
            enemy.destinationSetter.target = enemy.initialTransform;
            return typeof(ReturnState);
        }

        if (enemy.CheckTargetDistance() < Mathf.Pow(EnemySettings.AttackRange, 2))
        {
            if (attackTimerSet && attackTimer < 0f)
            {
                enemy.animator.SetTrigger("Attack");
                attackTimer = EnemySettings.AttackTimer;
            }
            else if (!attackTimerSet)
            {
                attackTimer = EnemySettings.AttackTimer;
                attackTimerSet = true;
            }
            attackTimer -= Time.deltaTime;           
        }

        if(attackTimerSet && enemy.CheckTargetDistance() > Mathf.Pow(EnemySettings.AttackRange, 2))
        {
            attackTimerSet = false;
        }


        return null;
    }

    public override void FixedTick()
    {
        if(enemy.player != null)
        {
            //enemy.Move(enemy.player.transform.position);
        }
        
    }
}
