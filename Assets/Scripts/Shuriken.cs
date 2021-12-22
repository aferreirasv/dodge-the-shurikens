using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb;
    public AudioSource shurikenWallHit;
    public AudioSource shurikenPlayerHit;
    public AudioSource shurikenIdleSound;
    public Animator animator;
    Vector2 movement;


    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "wall")
        {
            shurikenWallHit.Play();
        }
        if (collision.collider.tag == "player")
        {
            Player player = collision.rigidbody.GetComponent<Player>();
            if (player.Damage())
            {
                shurikenIdleSound.Stop();
                shurikenPlayerHit.Play();
                rb.velocity = new Vector2(0,0);
                collision.collider.attachedRigidbody.velocity = new Vector2(0, 0);
                animator.SetBool("HIT_PLAYER", true);
                rb.rotation = 35f;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (collision.collider.tag == "shuriken")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
       
    }
    public void Create(Vector2 position)
    {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.position = position;
        movement = new Vector2(gameManager.GetDirection() * gameManager.shurikenMaxSpeed, gameManager.GetDirection() * gameManager.shurikenMaxSpeed);
        rb.AddForce(movement * 10);
        animator.SetBool("HIT_PLAYER", false);
    }
}