using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    public WaypointType type;

    [ReadOnly] public bool reserved;

    [Foldout("Events")] public UnityEvent<Waypoint> onDestroy;

    void OnDestroy()
    {
        onDestroy.Invoke(this);
    }
}