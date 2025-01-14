using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] 
    public bool isPermanentMovingProhibition;
    
    [Header("Parameters")]
    public float moveSpeed = 3.5f;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource footAudioSource;

    private Vector3 direction;

    private bool isAbleToMove = true;

    private void Update()
    {
        if (isPermanentMovingProhibition)
        {
            isAbleToMove = false;
        }
        
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

            direction *= moveSpeed;
            rb.velocity = direction;
        }
    }

    public void PlayFootSound()
    {
        footAudioSource.Play();
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
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}