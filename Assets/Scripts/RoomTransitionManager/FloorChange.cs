using UnityEngine;

public class FloorChange : MonoBehaviour
{
    [SerializeField] private Transform targetPos;

    private void Start()
    {
        GetComponent<Door>().OnDoorInteract += OnDoorInteract;
    }

    private void OnDoorInteract(bool isDoorOpen)
    {
        if (targetPos == null)
            return;

        if(isDoorOpen)
            RoomTransitionManager.Instance.ChangeFloor(targetPos.position);
    }
}