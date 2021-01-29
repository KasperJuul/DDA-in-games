using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float turnSpeed = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    Vector2 moveDir;
    float sqrMag;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDir = Vector2.ClampMagnitude(new Vector2(playerInput.Horizontal, playerInput.Vertical), 1f);
        sqrMag = moveDir.SqrMagnitude();
        playerInput.animator.SetFloat("Speed", sqrMag);
    }


    private void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
        if (moveDir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }



    }
}
