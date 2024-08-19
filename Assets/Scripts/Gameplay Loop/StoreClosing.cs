using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class StoreClosing : MonoBehaviour
{
    [SerializeField] float moneyMultiplierPerDay = 0.1f;
    
    float moneyMultiplier = 1;

    public CloseStoreSummary CloseStore(Store store)
    {
        var summary = new CloseStoreSummary{
            money = store.GetMoney().GetValue(),
            infrastructureSold = store.GetInfrastructure().SellAll(),
            moneyMultiplier = moneyMultiplier
        };
        store.GetMoney().MultiplyValue(moneyMultiplier);
        summary.overallMoney = store.GetMoney().GetValue();
        return summary;
    }

    public void DayPassed()
    {
        moneyMultiplier += moneyMultiplierPerDay;
    }
}
public struct CloseStoreSummary
{
    public float money;
    public float infrastructureSold;
    public float moneyMultiplier;
    public float overallMoney;
}
