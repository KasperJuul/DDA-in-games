using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage = 50;
    [SerializeField] protected Transform attackPosition;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask whatIsEnemy;

    private PlayerInput playerInput;

    private void Awake()
    {
        GetComponentInParent<PlayerInput>().Onfire += Attack;
        GetComponentInParent<PlayerInput>().OnHit += DealDamage;
        playerInput = GetComponentInParent<PlayerInput>();
    }

    public virtual void Attack()
    {
        // Do animation stuff
        playerInput.animator.SetTrigger("Attack");
    }

    public virtual void DealDamage()
    {
        Collider2D[] objectsToHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
        if(objectsToHit.Length > 0)
        {
            Camera.main.GetComponent<CameraController>().CameraShake();
        }
        for (int i = 0; i < objectsToHit.Length; i++)
        {
            objectsToHit[i].GetComponent<Health>().TakeDamage(damage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
