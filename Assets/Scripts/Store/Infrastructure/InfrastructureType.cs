using UnityEngine;

[CreateAssetMenu(menuName = "Infrastucture")]
public class InfrastructureType : StoreObjectType
{
    public Optional<float> sellValue;
    public float valueLossPerDay = 0.05f;
    public float minOriginalValue = 0.5f;
}