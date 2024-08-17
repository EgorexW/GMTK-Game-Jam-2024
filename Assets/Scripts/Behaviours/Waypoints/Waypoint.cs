using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    public WaypointType type;

    [Foldout("Events")] public UnityEvent<Waypoint> onDestroy;

    void OnDestroy()
    {
        onDestroy.Invoke(this);
    }
}