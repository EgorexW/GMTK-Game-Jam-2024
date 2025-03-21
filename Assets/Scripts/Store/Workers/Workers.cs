using System.Collections.Generic;
using UnityEngine;

public class Workers : MonoBehaviour
{
    [SerializeField] int fireDelay = 3;
    
    Dictionary<WorkerType, List<Worker>> workersByType = new();
    List<Worker> workers = new();

    void Awake()
    {
        foreach (var worker in GetComponentsInChildren<Worker>()){
            AddWorker(worker);
        }
    }

    void AddWorker(Worker worker)
    {
        workers.Add(worker);
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
        worker.SetDaysTillFire(fireDelay);
    }

    public Dictionary<WorkerType, List<Worker>> GetWorkersByType()
    {
        return new(workersByType);
    }

    public void DayPassed()
    {
        foreach (var worker in workers.Copy()){
            if (worker.DayPassedCheckForFire()){
                RemoveWorker(worker);
            }
        }
    }

    void RemoveWorker(Worker worker)
    {
        workersByType[worker.GetWorkerType()].Remove(worker);
        if (workersByType[worker.GetWorkerType()].Count == 0){
            workersByType.Remove(worker.GetWorkerType());
        }
        workers.Remove(worker);
        worker.Remove();
    }
}