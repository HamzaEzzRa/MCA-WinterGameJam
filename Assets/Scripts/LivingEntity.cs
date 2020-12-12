using System;
using UnityEngine;

public class LivingEntity : Groundable
{
    public float startingHealth = 100f;
    protected float health;
    protected bool dead;
    public event Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    protected void TakeDamage(float damage)
    {
        health -= damage;
        if (InGameUI.Instance != null)
            InGameUI.Instance.SetHealth(health);
        if (health <= 0 && !dead)
            Die();
    }

    protected void Die()
    {
        dead = true;
        OnDeath?.Invoke();
    }
}
