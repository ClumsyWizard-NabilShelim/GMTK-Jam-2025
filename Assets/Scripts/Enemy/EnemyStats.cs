using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;

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