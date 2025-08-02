using ClumsyWizard.Core;
using UnityEngine;

public class LevelTriggerTransition : MonoBehaviour
{
    [SerializeField] private string targetScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Player player = collision.GetComponent<Player>();
        player.Toggle(false);
        CW_SceneManagement.Instance.Load(targetScene);
    }
}
