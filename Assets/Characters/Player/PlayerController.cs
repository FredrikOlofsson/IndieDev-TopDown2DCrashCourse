using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {

        }
    }

    [ContextMenu("TakeDamage")]
    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

    }
}
