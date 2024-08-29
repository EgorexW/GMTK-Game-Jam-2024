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
            waypoint.onDestroy.AddListener(RemoveWaypoint);
        }
    }

    void RemoveWaypoint(Waypoint waypoint)
    {
        waypointsByType[waypoint.type].Remove(waypoint);
    }

    public Waypoint GetWaypointOfType(WaypointType type)
    {
        var waypointsOfType = GetWaypointsOfType(type);
        return waypointsOfType.Random();
    }

    public List<Waypoint> GetWaypointsOfType(WaypointType type, bool ignoreReserved = false)
    {
        if (!type.reserveable || ignoreReserved){
            return waypointsByType[type];
        }
        var availableWaypoints = new List<Waypoint>();
        foreach (var waypoint in waypointsByType[type]){
            if (!waypoint.reserved){
                availableWaypoints.Add(waypoint);
            }
        }
        return availableWaypoints;
    }

    public List<Vector3> GetWaypointsPosOfType(WaypointType waypointType, bool ignoreReserved = false)
    {
        var waypoints = GetWaypointsOfType(waypointType, ignoreReserved);
        var poses = new List<Vector3>();
        foreach (Waypoint waypoint in waypoints){
            poses.Add(waypoint.transform.position);
        }
        return poses;
    }
}