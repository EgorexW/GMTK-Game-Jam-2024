using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Seeker), typeof(AIPath))]
public class WaypointBehaviour : GameBehaviour
{
    
    [SerializeField] [GetComponent] Seeker seeker;
    [SerializeField] [GetComponent] AIPath aiPath;
    
    [Required] [SerializeField] Transform transformToMove;
    [SerializeField] WaypointCollection waypointCollection;
    
    [SerializeField] float minTimeToWait = 0.1f;
    [SerializeField] float disToReachWaypoint = 0.2f;
    [SerializeField] float noWaypointRetryTime = 0.5f;
    
    [SerializeField] List<ObjectWithValue<WaypointType>> waypointsToFollow;
    [SerializeField] float waitTimeVariance = 1;
    [SerializeField] OnFinish onFinish;
    [SerializeField] WaypointType endWaypoint;

    [ReadOnly][SerializeField] State state = State.Finished;
    [ReadOnly][SerializeField] Waypoint nextWaypoint;
    [ReadOnly][SerializeField] float timeToWait;
    int currentIndex;

    void Awake()
    {
        state = State.Finished;
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

    void NextWaypoint()
    {
        currentIndex += 1;
        if (currentIndex >= waypointsToFollow.Count){
            Finish();
            return;
        }
        MoveToWaypoint(waypointsToFollow[currentIndex]);
    }
    void Update()
    {
        switch (state){
            case State.Finished:
                return;
            case State.Waiting:
                timeToWait -= GetDeltaTime();
                if (timeToWait <= 0){
                    NextWaypoint();
                }
                return;
            case State.NoWaypoint:
                timeToWait -= GetDeltaTime();
                if (timeToWait <= 0){
                    MoveToWaypoint(waypointsToFollow[currentIndex]);
                }
                return;
            case State.Moving:
            case State.Ending:
                if (!seeker.IsDone()){
                    return;
                }
                aiPath.MovementUpdate(GetDeltaTime(), out Vector3 nextPosition, out Quaternion nextRotation);
                transformToMove.position = nextPosition;
                transformToMove.rotation = nextRotation;
                var dis = Vector3.Distance(nextWaypoint.transform.position, transform.position);
                if (!(dis < disToReachWaypoint)) return;
                ReachedWaypoint();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void ReachedWaypoint()
    {
        if (state == State.Ending){
            state = State.Finished;
            return;
        }
        timeToWait = waypointsToFollow[currentIndex];
        timeToWait += Random.Range(-waitTimeVariance, waitTimeVariance);
        timeToWait = Mathf.Max(minTimeToWait, timeToWait);
        state = State.Waiting;
    }


    void Finish()
    {
        switch (onFinish){
            case OnFinish.End:
                End();
                break;
            case OnFinish.StayOnLast:
                state = State.Finished;
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
        state = State.Ending;
    }

    void MoveToWaypoint(WaypointType waypointType)
    {
        if (nextWaypoint != null) nextWaypoint.reserved = false;
        nextWaypoint = waypointCollection.GetWaypointOfType(waypointType);
        if (nextWaypoint == null){
            state = State.NoWaypoint;
            timeToWait = noWaypointRetryTime;
            return;
        }
        state = State.Moving;
        nextWaypoint.reserved = true;
        seeker.StartPath(transform.position, nextWaypoint.transform.position);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (nextWaypoint != null) nextWaypoint.reserved = false;
    }
}

enum OnFinish
{
    End,
    StayOnLast,
    Loop
}
enum State
{
    Moving,
    Waiting,
    NoWaypoint,
    Finished,
    Ending
}