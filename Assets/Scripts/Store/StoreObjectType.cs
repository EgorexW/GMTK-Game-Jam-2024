using System;
using UnityEngine;
using UnityEngine.Serialization;

public class StoreObjectType : ScriptableObject
{
    public Sprite sprite;
    public AttractivenessImpact attractivenessImpact;
    public ClientsCapImpact clientsCapImpact;
    public RevenueImpact revenueImpact;
    public CleannessImpact cleannessImpact;
    public string description;
}

[Serializable]
public struct CleannessImpact
{
    public float dirtGeneration;
}

[Serializable]
public struct RevenueImpact
{
    public float baseValue;
}

[Serializable]
public struct ClientsCapImpact
{
    public int cashingValue;
}

[Serializable]
public struct AttractivenessImpact
{
    public float baseValue;
}

public interface IStoreObject
{
    public StoreObjectType GetStoreObjectType();
}