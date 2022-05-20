using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth { get; private set; }
    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;
    public float currentHealth { get; private set; }
    private GameManager GM;

    private void Awake()
    {
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        GM = FindObjectOfType<GameManager>();
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        GM.Health(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void IncreaseHealth(float amount)
    {
        if (currentHealth != maxHealth)
        {
            currentHealth += amount;
            GM.Health(currentHealth);
        }
    }
    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }
}
