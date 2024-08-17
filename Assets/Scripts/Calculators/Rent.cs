using UnityEngine;

public class Rent : MonoBehaviour
{
    [SerializeField] float rentValue;

    public float Recalculate()
    {
        return rentValue;
    }
}