using UnityEngine;
using UnityEngine.Serialization;

public class ShopObjectType : ScriptableObject
{
    public Sprite sprite;
    public AttractivenessImpact attractivenessImpact;
}

public struct AttractivenessImpact
{
    public float baseValue;
    public float capValue;
}

public interface IShopObject
{
    public ShopObjectType GetShopObjectType();
}