using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public bool IsAbleToMove => isAbleToMove;

    [SerializeField] private float speed = 3.5f;

    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    private Animator animator => GetComponent<Animator>();
    private AudioSource footSound => GetComponent<AudioSource>();
    
    private Vector3 direction;
    private bool isAbleToMove;

    private void Awake() => isAbleToMove = true;
    
    private void Update()
    {
        if (isAbleToMove)
        {
            direction = Vector3.zero; 

            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
                FlipSprite();

                animator.SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
                FlipSprite();

                animator.SetBool("isWalking", true);
            }
            else 
            {
                animator.SetBool("isWalking", false);
            }

            direction *= speed;
            rb.velocity = direction;
        }
    }
    
    public void PlayFootSound()
    {
        footSound.Play();
    }

    public void ProhibitMovement()
    {
        isAbleToMove = false;
        animator.SetBool("isWalking", false);
    }
    public void AllowMovement()
    {
        isAbleToMove = true;
    }

    private void FlipSprite()
    {
        if (direction.x > 0)
                spriteRenderer.flipX = false;
            else if (direction.x < 0)
                spriteRenderer.flipX = true;
    }
}