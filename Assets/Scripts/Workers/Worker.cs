using System;
using NaughtyAttributes;
using UnityEngine;

public class Worker : MonoBehaviour
{ 
    [SerializeField][Required] WorkerType workerType;
    [SerializeField] Optional<int> daysTillFire;

    void Awake()
    {
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null){
            spriteRenderer.sprite = workerType.sprite;
        }
    }

    public WorkerType GetWorkerType()
    {
        return workerType;
    }

    public Optional<int> GetDaysTillFire()
    {
        return daysTillFire;
    }
}