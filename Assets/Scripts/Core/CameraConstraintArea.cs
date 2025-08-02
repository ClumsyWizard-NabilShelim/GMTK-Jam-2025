using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraConstraintArea : MonoBehaviour
{
    [SerializeField] private List<RailData> railPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        CameraRailSystem.Instance.SetConstraint(railPoints, false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        CameraRailSystem.Instance.SetConstraint(null, true);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < railPoints.Count; i++)
        {
            Gizmos.DrawSphere(railPoints[i].Start, 0.15f);
            Gizmos.DrawSphere(railPoints[i].End, 0.15f);
            Gizmos.DrawSphere((railPoints[i].End + railPoints[i].Start) / 2.0f, 0.2f);
            Handles.DrawLine(railPoints[i].Start, railPoints[i].End, 5.0f);
        }
    }
#endif
}