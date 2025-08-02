using UnityEngine;


public class CameraController : MonoBehaviour
{
    private Player player;

    [SerializeField] private Vector2 normalOffset;
    [SerializeField] private float normalSmoothing;
    [Space]
    [SerializeField] private Vector2 climbingOffset;
    [SerializeField] private float climbingSmoothing;

    private Vector3 refVelocity;

    private Vector3 originalNormalOffset;
    private Vector3 originalClimbingffset;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
        else
            print("No Player In Scene");

        originalNormalOffset = normalOffset;
        originalClimbingffset = climbingOffset;
    }

    private void Update()
    {
        if (player.Facing.x < 0.0f)
            normalOffset.x = -originalNormalOffset.x;
        else if (player.Facing.x > 0.0f)
            normalOffset.x = originalNormalOffset.x;

        if (InputManager.Instance.InputAxis.y < 0.0f)
            climbingOffset.y = -originalClimbingffset.y;
        else if (InputManager.Instance.InputAxis.y > 0.0f)
            climbingOffset.y = originalClimbingffset.y;
    }

    private void LateUpdate()
    {
        if(player.StateModifier.State == PlayerModifiedState.Climbing)
            transform.position = Vector3.SmoothDamp(transform.position, CameraRailSystem.Instance.GetFollowPoint() + (CameraRailSystem.Instance.CanOffset ? climbingOffset : Vector2.zero), ref refVelocity, climbingSmoothing);
        else
            transform.position = Vector3.SmoothDamp(transform.position, CameraRailSystem.Instance.GetFollowPoint() + (CameraRailSystem.Instance.CanOffset ? normalOffset : Vector2.zero), ref refVelocity, normalSmoothing);
    }
}