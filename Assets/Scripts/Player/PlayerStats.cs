using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    private Animator animator;
    [SerializeField] private int health;

    public void Initiaize(Player player)
    {

    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        
    }
}