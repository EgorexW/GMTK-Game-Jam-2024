using System;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollection : MonoBehaviour
{
    Dictionary<WaypointType, List<Waypoint>> waypointsByType = new();

    void Awake()
    {
        foreach (var waypoint in GetComponentsInChildren<Waypoint>()){
            if (!waypointsByType.ContainsKey(waypoint.type)){
                waypointsByType[waypoint.type] = new List<Waypoint>();
            }
            waypointsByType[waypoint.type].Add(waypoint);
        }
    }

    public Waypoint GetWaypointOfType(WaypointType type)
    {
        return GetWaypointsOfType(type).Random();
    }

    public List<Waypoint> GetWaypointsOfType(WaypointType type)
    {
        return waypointsByType[type];
    }
}