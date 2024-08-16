using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Infrastructure : MonoBehaviour
{
    [SerializeField] List<InfrastructureObject> startInfrastructure;

    Dictionary<InfrastructureType, List<InfrastructureObject>> infrastructureByType = new();
    
    void Awake()
    {
        foreach (var infrastructure in startInfrastructure){
            AddInfrastructure(infrastructure);
        }
    }

    void AddInfrastructure(InfrastructureObject infrastructure)
    {
        if (!infrastructureByType.TryGetValue(infrastructure.GetInfrastructureType(), out var infrastructureOfType)){
            infrastructureOfType = new List<InfrastructureObject>();
            infrastructureByType[infrastructure.GetInfrastructureType()] = infrastructureOfType;
        }
        infrastructureOfType.Add(infrastructure);
    }

    public List<InfrastructureObject> GetInfrastructureOfType(InfrastructureType infrastructureType)
    {
        return infrastructureByType[infrastructureType];
    }

    void Reset()
    {
        startInfrastructure = new List<InfrastructureObject>(GetComponentsInChildren<InfrastructureObject>());
    }
}