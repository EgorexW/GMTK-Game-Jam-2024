using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Infrastructure : MonoBehaviour
{
    [Required] [SerializeField] Money money;
    
    Dictionary<InfrastructureType, List<InfrastructureObject>> infrastructureByType = new();
    List<InfrastructureObject> infrastructureObjects = new();

    [Foldout("Events")] public UnityEvent<InfrastructureObject> onRemoveInfrastructure;

    void Awake()
    {
        foreach (var infrastructure in GetComponentsInChildren<InfrastructureObject>()){
            AddInfrastructure(infrastructure);
        }
    }

    void AddInfrastructure(InfrastructureObject infrastructure)
    {
        if (!infrastructureByType.TryGetValue(infrastructure.GetInfrastructureType(), out var infrastructureOfType)){
            infrastructureOfType = new List<InfrastructureObject>();
            infrastructureByType[infrastructure.GetInfrastructureType()] = infrastructureOfType;
        }
        infrastructureObjects.Add(infrastructure);
        infrastructureOfType.Add(infrastructure);
    }

    public List<InfrastructureObject> GetInfrastructureOfType(InfrastructureType infrastructureType)
    {
        return infrastructureByType[infrastructureType];
    }

    public float Sell(InfrastructureObject infrastructureObject)
    {
        var sellValue = infrastructureObject.GetSellValue();
        if (!sellValue){
            return 0;
        }
        money.ModifyValue(sellValue);
        infrastructureObject.Remove();
        RemoveInfrastructure(infrastructureObject);
        return sellValue;
    }

    void RemoveInfrastructure(InfrastructureObject infrastructureObject)
    {
        infrastructureObjects.Remove(infrastructureObject);
        infrastructureByType[infrastructureObject.GetInfrastructureType()].Remove(infrastructureObject);
        if (infrastructureByType[infrastructureObject.GetInfrastructureType()].Count == 0){
            infrastructureByType.Remove(infrastructureObject.GetInfrastructureType());
        }
        onRemoveInfrastructure.Invoke(infrastructureObject);
    }

    public float SellAll()
    {
        var value = 0f;
        foreach (var infrastructureObject in infrastructureObjects.Copy()){
            value += Sell(infrastructureObject);
        }
        return value;
    }

    public void DayPassed()
    {
        foreach (var infrastructureObject in infrastructureObjects){
            infrastructureObject.DayPassed();
        }
    }
}