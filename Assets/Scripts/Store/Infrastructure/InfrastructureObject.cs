using NaughtyAttributes;
using UnityEngine;

public class InfrastructureObject : MonoBehaviour, IStoreObject
{
    [Required] [SerializeField] InfrastructureType infrastructureType;
    [ReadOnly] [SerializeField] float originalValue = 1;

    public void DayPassed()
    {
        originalValue -= infrastructureType.valueLossPerDay;
        originalValue = Mathf.Max(originalValue, infrastructureType.minOriginalValue);
    }

    public Optional<float> GetSellValue()
    {
        var value = infrastructureType.sellValue * originalValue;
        return new Optional<float>(value, infrastructureType.sellValue);
    }
    public InfrastructureType GetInfrastructureType()
    {
        return infrastructureType;
    }

    public StoreObjectType GetStoreObjectType()
    {
        return infrastructureType;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}