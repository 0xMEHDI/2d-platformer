using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float lockOnSpeed = 2f;
    [SerializeField] float lockOnDistance = 5f;
    [SerializeField] float lockOnDeadZone = 1f;

    Transform player;
    Rigidbody2D rb2d;
    Animator animator;

    bool moving;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("moving", moving);
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(player.position.x - rb2d.position.x) < lockOnDistance && Mathf.Abs(player.position.y - rb2d.position.y) < lockOnDistance)
        {
            if (Mathf.Abs(player.position.x - rb2d.position.x) > lockOnDeadZone)
            {
                Vector2 target = new Vector2(player.position.x, rb2d.position.y);
                rb2d.position = Vector2.MoveTowards(rb2d.position, target, lockOnSpeed * Time.fixedDeltaTime);

                moving = true ? rb2d.position.x != player.position.x : moving = false;

                if (rb2d.position.x - player.position.x > Mathf.Epsilon)
                {
                    rb2d.transform.localScale = new Vector2(Mathf.Abs(rb2d.transform.localScale.x), rb2d.transform.localScale.y);
                }
                else if (rb2d.position.x - player.position.x < -Mathf.Epsilon)
                {
                    rb2d.transform.localScale = new Vector2(-Mathf.Abs(rb2d.transform.localScale.x), rb2d.transform.localScale.y);
                }
            }

            else
            {
                moving = false;
            }
        }

        else 
        {
            moving = false;
        }
    }
}
