using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    public bool IsDead => health <= 0;

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}