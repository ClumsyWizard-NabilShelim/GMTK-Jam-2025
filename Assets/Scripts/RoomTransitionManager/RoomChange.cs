using UnityEngine;

public class RoomChange : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private GameObject targetRoom;

    private void Start()
    {
        GetComponent<Door>().OnDoorInteract += OnDoorInteract;
    }

    private void OnDoorInteract(bool isDoorOpen)
    {
        if (targetPos == null || targetRoom == null)
            return;

        if (isDoorOpen)
            RoomTransitionManager.Instance.ChangeRoom(targetRoom, targetPos.position);
    }
}
