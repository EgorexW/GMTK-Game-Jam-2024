using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Workers : MonoBehaviour
{
    Dictionary<WorkerType, List<Worker>> workersByType = new();
    
    void Awake()
    {
        foreach (var worker in GetComponentsInChildren<Worker>()){
            AddWorker(worker);
        }
    }

    void AddWorker(Worker worker)
    {
        if (!workersByType.TryGetValue(worker.GetWorkerType(), out var workersOfType)){
            workersOfType = new List<Worker>();
            workersByType[worker.GetWorkerType()] = workersOfType;
        }
        workersOfType.Add(worker);
    }

    public List<Worker> GetWorkersOfType(WorkerType workerType)
    {
        return workersByType[workerType];
    }

    public void FireWorker(Worker worker)
    {
        worker.SetDaysTillFire(3); //TODO Remove magic nr
    }

    public Dictionary<WorkerType, List<Worker>> GetWorkersByType()
    {
        return new(workersByType);
    }
}