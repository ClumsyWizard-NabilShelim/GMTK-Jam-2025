using ClumsyWizard.Core;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    private Player player;
    [SerializeField] private int health;

    public bool IsDead => health <= 0;

    public void Initiaize(Player player)
    {
        this.player = player;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        player.Visuals.Trigger("Death");
        player.Toggle(false);
        StartCoroutine(DelayedLevelReload());
    }

    private IEnumerator DelayedLevelReload()
    {
        yield return new WaitForSeconds(2.0f);
        CW_SceneManagement.Instance.Reload();
    }
}