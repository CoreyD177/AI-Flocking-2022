using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints;

    public float radius = 2.5f;

    [SerializeField] private Vector3 gizmoSize = Vector3.one;
    public bool isFill = true;
    private void Start()
    {
        FillWithChildren();
    }
    private void FillWithChildren()
    {
        if (!isFill) return;

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                waypoints.Add(child);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if(waypoints == null || waypoints.Count == 0)
        {
            return;
        }
        for (int i = 0; i < waypoints.Count; i++)
        {
            Transform waypoint = waypoints[i];

            if (waypoint == null)
            {
                continue;
            }
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(waypoint.position, gizmoSize);

            Gizmos.color = Color.magenta;
            if (i + 1 < waypoints.Count && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoint.position, waypoints[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(waypoint.position, waypoints[0].position);
            }
        }
    }
}
