using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class CleannessBehaviour : GameBehaviour
{
    [Required][SerializeField] ObjectsFactory objectsFactory;

    [SerializeField] float changeTime = 60;
    [SerializeField] WaypointType waypointType;
    
    WaypointCollection waypoints;
    float spawnDelay;
    float timeSinceLastSpawn;
    bool positiveChange;
    List<Waypoint> unusedPositions;

    void Awake()
    {
        waypoints = General.GetRootComponent<WaypointCollection>(transform);
    }

    public void SetTargetCleanness(float targetCleanness)
    {
        unusedPositions = waypoints.GetWaypointsOfType(waypointType, false);
        targetCleanness = 1 - targetCleanness;
        var targetObjects = targetCleanness * unusedPositions.Count;
        var difference = targetObjects - objectsFactory.GetObjects().Count;
        positiveChange = difference > 0;
        spawnDelay = changeTime / Mathf.Abs(difference);
    }
    void Update()
    {
        if (!running){
            return;
        }
        timeSinceLastSpawn += GetDeltaTime();
        if (timeSinceLastSpawn >= spawnDelay){
            if (positiveChange){
                var obj = objectsFactory.AddObject();
                var random = unusedPositions.Random();
                if (random == null){
                    Debug.LogWarning("Ran out of unused positions", this);
                }
                unusedPositions.Remove(random);
                obj.transform.position = random.transform.position;
            }
            else{
                objectsFactory.RemoveObject();
            }
            timeSinceLastSpawn = 0;
        }
    }
}