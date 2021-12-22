using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 movement;
    public bool dead = false;
    public Rigidbody2D rb;
    public int moveSpeed = 30;
    float speedFactor = 0.01f;
    public GameManager gameManager;
    public GameObject ShieldObject;
    public bool shielded = false;
    public AudioSource shieldDestroyed;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    public void AddSpeed(int speed)
    {
        this.moveSpeed += speed;
        Debug.Log(moveSpeed);
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            rb.MovePosition(rb.position + movement * MoveSpeed());
        }
    }

    private float MoveSpeed()
    {
        return moveSpeed * speedFactor;
    }

    public bool Damage()
    {
        if (shielded)
        {
            SetShield(false);
            shieldDestroyed.Play();
            return false;
        }
        else
        {
            if (!dead)
            {
                Kill();
            }
            return true;
        }
    }

    public void SetShield(bool shield)
    {
        shielded = shield;
        ShieldObject.SetActive(shield);
    }

    public void Kill()
    {
        dead = true;
        gameManager.GameOver();
    }

}
