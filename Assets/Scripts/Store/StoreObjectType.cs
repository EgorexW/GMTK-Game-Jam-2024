using System;
using UnityEngine;
using UnityEngine.Serialization;

public class StoreObjectType : ScriptableObject
{
    public Sprite sprite;
    public AttractivenessImpact attractivenessImpact;
}

[Serializable]
public struct AttractivenessImpact
{
    public float baseValue;
    public float capValue;
}

public interface IStoreObject
{
    public StoreObjectType GetShopObjectType();
}