using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class WorkersSalary : MonoBehaviour
{
    [FormerlySerializedAs("costs")] [ReadOnly][SerializeField] float workersSalary;

    public float Recalculate(Workers workers)
    {
        workersSalary = Calculate(workers);
        return workersSalary;
    }
    float Calculate(Workers workers)
    {
        var value = 0f;
        foreach (var workerByType in workers.GetWorkersByType()){
            var workerType = workerByType.Key;
            var count = workerByType.Value.Count;
            value += workerType.salary * count;
        }
        return value;
    }
}