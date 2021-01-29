using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public Collider2D colliderA;
    public Collider2D colliderB;

    private void Awake()
    {
        Physics2D.IgnoreCollision(colliderA, colliderB);
    }
}
