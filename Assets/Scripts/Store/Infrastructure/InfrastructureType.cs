using UnityEngine;

[CreateAssetMenu(menuName = "Infrastucture")]
public class InfrastructureType : StoreObjectType
{
    public Optional<float> sellValue;
    public float valueLossPerDay = 0.1f;
    public float minOriginalValue = 0.3f;
}