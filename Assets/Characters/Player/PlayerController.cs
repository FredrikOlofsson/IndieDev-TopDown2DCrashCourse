using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public SpriteRenderer flipper;

    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    Vector2 movementInput;

    Rigidbody2D rb;

    Animator animator;


    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flipper = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool sucessfulMovement = TryMove(movementInput);

            if (!sucessfulMovement)
            {
                sucessfulMovement = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!sucessfulMovement)
            {
                sucessfulMovement = TryMove(new Vector2(0, movementInput.y));
            }
            
            animator.SetBool("isMoving", sucessfulMovement);
            FlipCharacter();
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void FlipCharacter()
    {   
        if (movementInput.x < 0)
        {
            flipper.flipX = true;
        } else if (movementInput.x > 0)
        {
            flipper.flipX = false;
        }
    }
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            //check for collisions
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
            //move the player
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
        private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
