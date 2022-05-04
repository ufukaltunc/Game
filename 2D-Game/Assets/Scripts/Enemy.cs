using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        animator.SetBool("Death", true);

        rb.bodyType = RigidbodyType2D.Kinematic;

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 2f);

        this.enabled = false;
    }
}
