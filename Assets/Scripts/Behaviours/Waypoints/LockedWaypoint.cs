using NaughtyAttributes;
using UnityEngine;

public class LockedWaypoint : Waypoint
{
    [Required][SerializeField] Waypoint waypointToUnlock;

    bool locked = true;
    
    void Awake()
    {
        reserved = true;
    }

    void Update()
    {
        if (locked){
            reserved = true;
        }
        if (locked && waypointToUnlock.reserved){
            locked = false;
            reserved = false;
        }
        if (!locked && !waypointToUnlock.reserved){
            locked = true;
        }
    }
}