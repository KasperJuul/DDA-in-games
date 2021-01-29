using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 5f;

    public override void Attack()
    {
        Debug.Log("RANGED ATTACK");
        GameObject arrow = Instantiate(arrowPrefab, attackPosition.position, transform.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = transform.up * arrowSpeed;
    }
}
