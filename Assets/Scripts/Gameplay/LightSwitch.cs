using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private Lights[] connectedLights;
    [SerializeField] private bool isOn;

    [Header("Visuals")]
    [SerializeField] private InteractableObjectUI ui;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closeSprite;

    private bool playerNear;

    private void Start()
    {
        InputManager.Instance.OnInteract += OnInteract;

        Toggle(isOn);
    }

    private void Toggle(bool active)
    {
        isOn = active;

        if (isOn)
            spriteRenderer.sprite = openSprite;
        else
            spriteRenderer.sprite = closeSprite;

        ToggleLights();
    }

    private void ToggleLights()
    {
        if (connectedLights == null || connectedLights.Length == 0)
            return;

        for (int i = 0; i < connectedLights.Length; i++)
        {
            connectedLights[i].Toggle(isOn);
        }
    }

    private void OnInteract()
    {
        if (!playerNear)
            return;

        Toggle(!isOn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerNear = true;
        ui.ToggleMarker(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerNear = false;
        ui.ToggleMarker(false);
    }

    //Clean up
    private void OnDestroy()
    {
        InputManager.Instance.OnInteract -= OnInteract;
    }
}