using UnityEngine;

public class Costs : MonoBehaviour
{
    public float Recalculate(float workersSalary, float rent)
    {
        return workersSalary + rent;
    }
}