using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public bool FireWeapons { get; private set; }
    public Animator animator;

    public event Action Onfire = delegate { };
    public event Action OnHit = delegate { };

    private float attackTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        FireWeapons = Input.GetButtonDown("Fire1");
        if (FireWeapons && attackTimer <= 0)
        {
            Onfire();
            attackTimer = 0.15f;
        }
        attackTimer -= Time.deltaTime;
    }

    public void Hit()
    {
        OnHit();
    }
}
