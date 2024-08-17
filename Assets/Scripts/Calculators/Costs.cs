using NaughtyAttributes;
using UnityEngine;

public class Costs : MonoBehaviour
{
    [ReadOnly][SerializeField] float costs;

    public float Recalculate(Workers workers)
    {
        costs = Calculate(workers);
        return costs;
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