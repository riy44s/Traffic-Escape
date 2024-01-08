using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Header("Waypoint status")]
    public Waypoints previousWaypoint;
    public Waypoints nextWaypoint;

    [Range(0f, 5f)]
    public float waypointWidth = 1f;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * waypointWidth / 2f;
        Vector3 maxBound = transform.position - transform.right * waypointWidth / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }
}
