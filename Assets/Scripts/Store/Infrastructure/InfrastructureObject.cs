using NaughtyAttributes;
using UnityEngine;

public class InfrastructureObject : MonoBehaviour, IStoreObject
{
    [Required] [SerializeField] InfrastructureType infrastructureType;

    public InfrastructureType GetInfrastructureType()
    {
        return infrastructureType;
    }

    public StoreObjectType GetShopObjectType()
    {
        return infrastructureType;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}