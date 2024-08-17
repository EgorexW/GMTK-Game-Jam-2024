using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class ClientsPerDay : MonoBehaviour
{
    [SerializeField] float decreasePerDay = 0.1f;
    [SerializeField] float minAttractiveness = 0.1f;
    [SerializeField] float attractivenessMod = 1f;
    
    [ReadOnly][SerializeField] int clientsPerDay;


    public int Recalculate(float attractiveness, float clientsCap)
    {
        clientsPerDay = Calculate(attractiveness, clientsCap);
        return clientsPerDay;
    }
    int Calculate(float attractiveness, float clientsCap)
    {
        var value = attractiveness * attractivenessMod;
        var cappedValue = Mathf.Min(value, clientsCap);
        return Mathf.RoundToInt(cappedValue);
    }

    public void DayPassed()
    {
        attractivenessMod -= decreasePerDay;
        attractivenessMod = Mathf.Max(attractivenessMod, minAttractiveness);
    }
}