using ClumsyWizard.Core;
using UnityEngine;

public class RoomTransitionManager : CW_Singleton<RoomTransitionManager>
{
    private Animator animator;
    private Player player;
    private Vector2 targetPos;
    [SerializeField] private GameObject currentActiveRoom;
    private GameObject currentTargetRoom;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    public void ChangeFloor(Vector2 targetPos)
    {
        this.targetPos = targetPos;

        player.Toggle(false);
        animator.SetBool("Fade", true);
        Invoke("Teleport", 0.6f);
    }

    public void ChangeRoom(GameObject targetRoom, Vector2 targetPos)
    {
        this.targetPos = targetPos;
        currentActiveRoom = targetRoom;

        player.Toggle(false);
        animator.SetBool("Fade", true);
        Invoke("SwitchRooms", 0.6f);
    }
    private void SwitchRooms()
    {
        currentActiveRoom.SetActive(false);
        currentTargetRoom.SetActive(true);
        currentActiveRoom = currentTargetRoom;
        Invoke("Teleport", 0.1f);
    }

    //Helper Functions
    private void Teleport()
    {
        player.RB.position = targetPos + new Vector2(0.0f, 1.0f);
        Invoke("HidePanel", 0.5f);
    }
    private void HidePanel()
    {
        animator.SetBool("Fade", false);
        Invoke("ReleasePlayer", 0.5f);
    }

    private void ReleasePlayer()
    {
        player.Toggle(true);
    }
}