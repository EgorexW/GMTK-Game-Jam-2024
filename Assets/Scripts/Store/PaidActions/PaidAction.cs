using UnityEngine;

public abstract class PaidAction : MonoBehaviour
{
    public bool activatedThisDay;
    public string description;
    
    [SerializeField] float buyCost;

    public float Buy(Store store)
    {
        Activate(store);
        return buyCost;
    }

    public void IncreaseCost()
    {
        buyCost += buyCost; //TODO decide on cost increase algorythm
    }
    protected abstract void Activate(Store store);

    public float GetCost()
    {
        return buyCost;
    }
}