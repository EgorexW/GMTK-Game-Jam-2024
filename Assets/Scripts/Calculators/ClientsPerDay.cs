using NaughtyAttributes;
using UnityEngine;

public class ClientsPerDay : MonoBehaviour
{
    [SerializeField] float decreasePerDay = 0.05f;
    [SerializeField] float minAttractiveness = 0.1f;
    [SerializeField] float attractivenessMod = 1f;
    
    [ReadOnly][SerializeField] int clientsPerDay;


    public int Recalculate(float attractiveness, float dirt)
    {
        clientsPerDay = Calculate(attractiveness, dirt);
        return clientsPerDay;
    }
    int Calculate(float attractiveness, float dirt)
    {
        var value = attractiveness * attractivenessMod;
        value -= dirt;
        return Mathf.CeilToInt(value);
    }

    public void DayPassed()
    {
        attractivenessMod -= decreasePerDay;
        attractivenessMod = Mathf.Max(attractivenessMod, minAttractiveness);
    }
}