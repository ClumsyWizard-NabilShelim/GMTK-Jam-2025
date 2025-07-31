using UnityEngine;


public class CameraController : MonoBehaviour
{
    private Player player;

    [SerializeField] private Vector2 offset;
    [SerializeField] private float smoothing;
    private Vector3 refVelocity;
    private Vector3 originalOffset;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
        else
            print("No Player In Scene");

        originalOffset = offset;
    }

    private void Update()
    {
        if (InputManager.Instance.InputAxis.x < 0.0f)
            offset.x = -originalOffset.x;
        else if (InputManager.Instance.InputAxis.x > 0.0f)
            offset.x = originalOffset.x;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, CameraRailSystem.Instance.GetFollowPoint() + (player.State == PlayerState.Climb? Vector2.zero : offset), ref refVelocity, smoothing);
    }
}