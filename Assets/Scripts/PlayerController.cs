using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsHandler
{
    [SerializeField] float maxSpeed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float knockbackForce = 100.0f;

    [SerializeField] int playerHealth = 3;

    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip damageSound;

    Vector2 move;
    bool canMove = true;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void ComputeVelocity()
    {
        if (canMove)
        {
            move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpForce;
            }

            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > Mathf.Epsilon)
                {
                    velocity.y *= 0.5f;
                }
            }

            targetVelocity = move * maxSpeed;
        }
        
        //TODO Refactor
        if (move.x > Mathf.Epsilon)
        {
            transform.localScale = new Vector2((Mathf.Abs(transform.localScale.x)), transform.localScale.y);
        }
        else if (move.x < -Mathf.Epsilon)
        {
            transform.localScale = new Vector2(-(Mathf.Abs(transform.localScale.x)), transform.localScale.y);
        }

        //TODO Refactor
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
    }

    //TODO Refactor
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canMove)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Vector2 direction = (Vector2)(transform.position - collision.transform.position).normalized;
                Vector2 knockback = direction * knockbackForce * Time.fixedDeltaTime;
                rb2d.position += knockback;

                if (playerHealth > 1)
                { 
                    playerHealth--;
                    audioSource.PlayOneShot(damageSound);
                }

                else if (playerHealth <= 1)
                {
                    playerHealth--;
                    StartDeathSequence();
                }
            }

            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                StartDeathSequence();
            }
        }
    }

    private void StartDeathSequence()
    {
        playerHealth = 0;
        audioSource.Play();
        canMove = false;
        animator.SetBool("dead", true);
        Invoke("ReloadLevel", 3f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
}
