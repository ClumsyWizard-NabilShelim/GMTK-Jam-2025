using UnityEngine;

public class PlayerEdgeDetector : MonoBehaviour
{
    [SerializeField] private float checkRange;
    [SerializeField] private LayerMask checkLayer;
    [SerializeField] private Vector2 topCheckPos;
    [SerializeField] private Vector2 bottomCheckPos;

    private RaycastHit2D hit;

    private Vector2 checkDir = new Vector2(1.0f, 0.0f);

    private void Update()
    {
        if (InputManager.Instance.InputAxis.x > 0.0f)
            checkDir.x = 1.0f;
        else if (InputManager.Instance.InputAxis.x < 0.0f)
            checkDir.x = -1.0f;
    }

    public bool IsNearLedge()
    {
        if (!Physics2D.Raycast((Vector2)transform.position + topCheckPos, checkDir, checkRange, checkLayer))
        {
            if (Physics2D.Raycast((Vector2)transform.position + bottomCheckPos, checkDir, checkRange, checkLayer))
            {
                hit = Physics2D.Raycast((Vector2)transform.position + topCheckPos + checkDir * checkRange * 2.0f, Vector2.down, 100.0f, checkLayer);
                return true;
            }
        }

        return false;
    }

    public Vector2 GetLedgeTopTargetPos()
    {
        if (hit.collider == null)
            return (Vector2)transform.position + topCheckPos + checkDir * checkRange * 2.0f;
        else
            return hit.point;
    }

    public Vector2 GetLedgeTopPos()
    {
        if (hit.collider == null)
            return (Vector2)transform.position + topCheckPos;
        else
            return hit.point - checkDir * checkRange;
    }

    //Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay((Vector2)transform.position + topCheckPos, checkDir * checkRange);
        Gizmos.DrawRay((Vector2)transform.position + bottomCheckPos, checkDir * checkRange);
    }
}
