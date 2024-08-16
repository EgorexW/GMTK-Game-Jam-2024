using NaughtyAttributes;
using UnityEngine;

public class InfrastructureObject : MonoBehaviour
{
    [Required] [SerializeField] InfrastructureType infrastructureType;

    public InfrastructureType GetInfrastructureType()
    {
        return infrastructureType;
    }
}