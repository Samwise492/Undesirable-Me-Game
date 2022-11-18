using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public AudioSource footSound;
    Vector3 direction;
    [HideInInspector] public bool isAbleToMove;

    void Awake() => isAbleToMove = true;
    void Update()
    {
        if (isAbleToMove)
        {
            direction = Vector3.zero; 
            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
                animator.SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
                animator.SetBool("isWalking", true);
            }
            else animator.SetBool("isWalking", false);

            direction *= speed;
            rb.velocity = direction;

            if (direction.x > 0) // if speed more than 0
                spriteRenderer.flipX = false; // then cancel reflection effect
            if (direction.x < 0)
                spriteRenderer.flipX = true;
        }
        else if (!isAbleToMove) 
            this.GetComponent<Animator>().SetBool("isWalking", false);
    }
    public void PlayFootSound()
    {
        footSound.Play();
    }
}
