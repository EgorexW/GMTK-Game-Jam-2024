using System;
using NaughtyAttributes;
using UnityEngine;

public class Worker : MonoBehaviour, IStoreObject
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

    public void SetDaysTillFire(Optional<int> value)
    {
        daysTillFire = value;
    }

    public StoreObjectType GetShopObjectType()
    {
        return workerType;
    }
}