using NaughtyAttributes;
using UnityEngine;

class Revenue : MonoBehaviour
{
    [ReadOnly][SerializeField] float revenue;
    
    public float Recalculate(float revenuePerClientValue, float clients)
    {
        revenue = Calculate(revenuePerClientValue, clients);
        return revenue;
    }

    float Calculate(float revenuePerClientValue, float clients)
    {
        var value = revenuePerClientValue * clients;
        return value;
    }
}