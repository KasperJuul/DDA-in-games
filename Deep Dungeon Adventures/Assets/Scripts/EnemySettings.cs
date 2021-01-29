using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemySettings : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    public static float MoveSpeed => Instance.moveSpeed;

    [SerializeField] private float aggroRadius = 3f;
    public static float AggroRadius => Instance.aggroRadius;

    [SerializeField] private float attackRange = 0.2f;
    public static float AttackRange => Instance.attackRange;

    [SerializeField] public float attackTimer = 1.5f;
    public static float AttackTimer => Instance.attackTimer;

    [SerializeField] public float bossAttackTimer = 1.5f;
    public static float BossAttackTimer => Instance.bossAttackTimer;

    public static EnemySettings Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (attackTimer < 0.25f)
            attackTimer = 0.25f;

        if (bossAttackTimer < 0.5f)
            bossAttackTimer = 0.5f;
    }
}
