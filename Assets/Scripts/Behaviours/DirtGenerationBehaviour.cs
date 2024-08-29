using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DirtGenerationBehaviour : GameBehaviour
{
    [Required][SerializeField] ObjectsFactory objectsFactory;

    [SerializeField] float changeTime = 60;
    [SerializeField] WaypointType waypointType;

    float spawnDelay;
    float timeSinceLastSpawn;
    List<Vector3> unusedPositions;
    int difference;

    void Start()
    {
        unusedPositions = General.GetRootComponent<WaypointCollection>(transform).GetWaypointsPosOfType(waypointType);
        objectsFactory.onRemoveObject.AddListener(OnRemoveDirt);
    }

    void OnRemoveDirt(GameObject arg0)
    {
        unusedPositions.Add(arg0.transform.position);
    }

    public void Generate(float newDirt)
    {
        difference = (int)newDirt;
        var positiveChange = difference > 0;
        if (!positiveChange){
            Debug.LogWarning("Can only generate more dirt", this);
            return;
        }
        spawnDelay = changeTime / Mathf.Abs(difference);
    }

    public void ChangeValue(ValueChangeCallback valueChangeCallback)
    {
        objectsFactory.SetCount((int)valueChangeCallback.newValue);
    }

    void Update()
    {
        if (!running){
            return;
        }
        timeSinceLastSpawn += GetDeltaTime();
        if (!(timeSinceLastSpawn >= spawnDelay)) return;
        if (difference <= 0) return;
        timeSinceLastSpawn = 0;
        if (unusedPositions.Count < 1){
            Debug.LogWarning("Ran out of unused positions", this);
            return;
        }
        difference -= 1;
        GameObject obj = objectsFactory.AddObject();
        Vector3 random = unusedPositions.Random();
        unusedPositions.Remove(random);
        obj.transform.position = random;
    }
}