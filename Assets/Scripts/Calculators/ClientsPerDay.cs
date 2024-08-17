using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class ClientsPerDay : MonoBehaviour
{
    [SerializeField] float decreasePerDay = 0.1f;
    [SerializeField] float minAttractiveness = 0.1f;
    [SerializeField] float attractivenessMod = 1f;
    
    [ReadOnly][SerializeField] float clientsPerDay;


    public float Recalculate(float attractiveness, float clientsCap)
    {
        clientsPerDay = Calculate(attractiveness, clientsCap);
        return clientsPerDay;
    }
    float Calculate(float attractiveness, float clientsCap)
    {
        var value = attractiveness * attractivenessMod;
        var cappedValue = Mathf.Min(value, clientsCap);
        return cappedValue;
    }

    public void DayPassed()
    {
        attractivenessMod -= decreasePerDay;
        attractivenessMod = math.max(attractivenessMod, minAttractiveness);
    }
}