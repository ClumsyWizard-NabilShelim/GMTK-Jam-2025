using System.Collections.Generic;
using UnityEngine;
using System;
using ClumsyWizard.Core;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct RailData
{
    public Vector2 Start;
    public Vector2 End;
}

public class CameraRailSystem : CW_Singleton<CameraRailSystem>
{
    public bool CanOffset { get; private set; }
    [SerializeField] private List<RailData> railPoints;
    private List<RailData> currentRailPoints;

    private RailData currentRail;
    private Transform player;

    private void Start()
    {
        CanOffset = true;
        currentRailPoints = railPoints;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public Vector2 GetFollowPoint()
    {
        FindClosestRail();
        return new Vector2(Mathf.Clamp(player.position.x, currentRail.Start.x, currentRail.End.x), Mathf.Clamp(player.position.y, currentRail.Start.y, currentRail.End.y));
    }

    //Helper Functions
    private void FindClosestRail()
    {
        RailData current = currentRailPoints[0];
        float minDist = Vector2.Distance(player.position, ((current.End + current.Start) / 2.0f));
        for (int i = 1; i < currentRailPoints.Count; i++)
        {
            float dist = Vector2.Distance(player.position, ((currentRailPoints[i].Start + currentRailPoints[i].End) / 2.0f));

            if (dist < minDist)
            {
                minDist = dist;
                current = currentRailPoints[i];
            }
        }

        currentRail = current;
    }

    public void SetConstraint(List<RailData> constraints, bool canOffset)
    {
        CanOffset = canOffset;

        if (constraints == null || constraints.Count == 0)
            currentRailPoints = railPoints;
        else
            currentRailPoints = constraints;
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