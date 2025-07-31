using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothing;
    private Vector3 refVelocity;
    private Vector3 originalOffset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
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
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref refVelocity, smoothing);
    }
}