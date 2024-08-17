using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker), typeof(AIPath))]
public class WaypointBehaviour : GameBehaviour
{
    const float DIS_TO_REACH_WAYPOINT = 0.2f;
    
    [SerializeField] [GetComponent] Seeker seeker;
    [SerializeField] [GetComponent] AIPath aiPath;
    
    [Required] [SerializeField] Transform transformToMove;
    [SerializeField] WaypointCollection waypointCollection;
    
    [SerializeField] List<ObjectWithValue<WaypointType>> waypointsToFollow;
    [SerializeField] OnFinish onFinish;
    [SerializeField] WaypointType endWaypoint;

    [ReadOnly][SerializeField] Waypoint nextWaypoint;
    [ReadOnly][SerializeField] float timeToWait;
    int currentIndex;

    void Awake()
    {
        if (waypointCollection == null){
            waypointCollection = General.GetRootComponent<WaypointCollection>(gameObject);
        }
        aiPath.canMove = false;
    }

    public override void Run(IBehaviourRunner runner)
    {
        base.Run(runner);
        Begin();
    }

    void Begin()
    {
        currentIndex = -1;
        NextWaypoint();
    }

    void Update()
    {
        if (nextWaypoint == null){
            return;
        }
        if (timeToWait > 0){
            timeToWait -= GetDeltaTime();
            if (timeToWait <= 0){
                NextWaypoint();
            }
            return;
        }
        if (!seeker.IsDone()){
            return;
        }
        aiPath.MovementUpdate(GetDeltaTime(), out Vector3 nextPosition, out Quaternion nextRotation);
        transformToMove.position = nextPosition;
        transformToMove.rotation = nextRotation;
        if (!running){
            return;
        }
        var dis = Vector3.Distance(nextWaypoint.transform.position, transform.position);
        if (!(dis < DIS_TO_REACH_WAYPOINT)) return;
        ReachedWaypoint();
    }

    void ReachedWaypoint()
    {
        timeToWait = waypointsToFollow[currentIndex];
    }

    void NextWaypoint()
    {
        currentIndex++;
        if (currentIndex >= waypointsToFollow.Count){
            Finish();
            return;
        }
        MoveToWaypoint(waypointsToFollow[currentIndex]);
    }

    void Finish()
    {
        switch (onFinish){
            case OnFinish.End:
                End();
                break;
            case OnFinish.StayOnLast:
                break;
            case OnFinish.Loop:
                Begin();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void End()
    {
        base.End();
        MoveToWaypoint(endWaypoint);
    }

    void MoveToWaypoint(WaypointType waypointType)
    {
        timeToWait = 0;
        nextWaypoint = waypointCollection.GetWaypointOfType(waypointType);
        seeker.StartPath(transform.position, nextWaypoint.transform.position);
    }
}

enum OnFinish
{
    End,
    StayOnLast,
    Loop
}