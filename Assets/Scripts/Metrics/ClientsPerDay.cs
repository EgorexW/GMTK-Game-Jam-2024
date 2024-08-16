using NaughtyAttributes;
using UnityEngine;

public class ClientsPerDay : MonoBehaviour
{
    public float attractivenessMod = 1f;
    [ReadOnly][SerializeField] float clientsPerDay;


    public float Recalculate(float attractiveness)
    {
        clientsPerDay = Calculate(attractiveness);
        return clientsPerDay;
    }
    float Calculate(float attractiveness)
    {
        return attractiveness * attractivenessMod;
    }
}