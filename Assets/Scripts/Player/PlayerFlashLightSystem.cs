using UnityEngine;

public class PlayerFlashLightSystem : MonoBehaviour
{
    private Player player;
    public bool IsActive => flashlight.activeSelf;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private Transform rotTransform;

    public void Initialize(Player player)
    {
        this.player = player;
        InputManager.Instance.OnToggleFlashLight += OnToggleFlashLight;
        flashlight.SetActive(false);
    }

    private void Update()
    {
        Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rotTransform.position;
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rotTransform.localEulerAngles = new Vector3(0.0f, 0.0f, rotZ - 90.0f);
    }

    private void OnToggleFlashLight()
    {
        if(PlayerInventory.Instance.HasItem("FlashLight") && !PlayerRestrictionManager.Instance.IsRestricted(ControlRestriction.FlashLight))
            flashlight.SetActive(!flashlight.activeSelf);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnToggleFlashLight -= OnToggleFlashLight;
    }
}