using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Workers : MonoBehaviour
{
    [SerializeField] List<Worker> startWorkers;

    Dictionary<WorkerType, List<Worker>> workersByType = new();
    
    void Awake()
    {
        foreach (var worker in startWorkers){
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

    void Reset()
    {
        startWorkers = new List<Worker>(GetComponentsInChildren<Worker>());
    }

    public void FireWorker(Worker worker)
    {
        worker.SetDaysTillFire(3); //TODO Remove magic nr
    }
}