using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class ClientsPerDay : MonoBehaviour
{
    [SerializeField] float decreasePerDay = 0.1f;
    [SerializeField] float minAttractiveness = 0.1f;
    [SerializeField] float attractivenessMod = 1f;
    
    [ReadOnly][SerializeField] int clientsPerDay;


    public int Recalculate(float attractiveness)
    {
        clientsPerDay = Calculate(attractiveness);
        return clientsPerDay;
    }
    int Calculate(float attractiveness)
    {
        var value = attractiveness * attractivenessMod;
        return Mathf.CeilToInt(value);
    }

    public void DayPassed()
    {
        attractivenessMod -= decreasePerDay;
        attractivenessMod = Mathf.Max(attractivenessMod, minAttractiveness);
    }
}