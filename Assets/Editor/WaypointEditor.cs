using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(Waypoints waypoints, GizmoType gizmoType)
    {
        if (waypoints == null)
        {
            // Handle the case where waypoints is null (maybe log a warning or return early)
            return;
        }

        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.blue * 0.4f;
        }

        Gizmos.DrawSphere(waypoints.transform.position, -.3f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoints.transform.position + (waypoints.transform.right * waypoints.waypointWidth / 2f),
                        waypoints.transform.position - (waypoints.transform.right * waypoints.waypointWidth / 2f));

        if (waypoints.previousWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 offset = waypoints.transform.right * waypoints.waypointWidth / 2f;
            Vector3 offsetTo = waypoints.previousWaypoint.transform.right * waypoints.previousWaypoint.waypointWidth / 2f;
            Gizmos.DrawLine(waypoints.transform.position + offset, waypoints.previousWaypoint.transform.position + offsetTo);
        }

        if (waypoints.nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 offset = waypoints.transform.right * waypoints.waypointWidth / 2f;
            Vector3 offsetTo = waypoints.nextWaypoint.transform.right * waypoints.nextWaypoint.waypointWidth / 2f;
            Gizmos.DrawLine(waypoints.transform.position + offset, waypoints.nextWaypoint.transform.position + offsetTo);
        }
    }
}
