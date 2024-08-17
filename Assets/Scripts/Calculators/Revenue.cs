using UnityEngine;

class Revenue : MonoBehaviour
{
    public float Recalculate(float revenuePerClientValue, float clients)
    {
        var value = Calculate(revenuePerClientValue, clients);
        return value;
    }

    float Calculate(float revenuePerClientValue, float clients)
    {
        var value = revenuePerClientValue * clients;
        return value;
    }
}