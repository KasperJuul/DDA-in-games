using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyInput: MonoBehaviour
{
    [SerializeField] public PlayerInput player;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public float speed = 3f;
    [SerializeField] public int damage = 25;
    public Vector3 initialPos;
    public Transform initialTransform;
    public Quaternion initialRot;
    private Vector3 playerDirection;
    public Animator animator;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform attackPosition;

    public AIPath aIPath;
    public AIDestinationSetter destinationSetter;

    public StateMachine StateMachine => GetComponent<StateMachine>();
    public DDA playerGauss;

    private void Awake()
    {
        InitializeStateMachine();
        animator = GetComponent<Animator>();
        aIPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        playerGauss = FindObjectOfType<DDA>();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(IdleState), new IdleState( _enemy: this) },
            {typeof(ChaseState), new ChaseState( _enemy: this) },
            {typeof(ReturnState), new ReturnState( _enemy: this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    public float CheckTargetDistance()
    {
        if (player == null)
        {
            return 1000f;
        }
        playerDirection = player.transform.position - transform.position;
        return playerDirection.sqrMagnitude;
    }

    public void Move(Vector3 destination)
    {
        Vector2 direction = destination - transform.position;
        rb.MovePosition(rb.position + Vector2.ClampMagnitude(direction, 1f) * speed * Time.fixedDeltaTime);
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void DealDamage()
    {
        Collider2D[] objectsToHit = Physics2D.OverlapCircleAll(attackPosition.position, 0.5f, whatIsPlayer);
        for (int i = 0; i < objectsToHit.Length; i++)
        {
            objectsToHit[i].GetComponent<Health>().TakeDamage(damage);
            playerGauss.accumulatedDamage += damage;
        }

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, 5.5f);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(attackPosition.position, 0.6f);
    //}

}
