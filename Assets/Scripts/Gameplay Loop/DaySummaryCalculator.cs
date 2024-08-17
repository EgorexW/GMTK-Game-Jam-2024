using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class DaySummaryCalculator : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] StoreAttractiveness storeAttractiveness;
    [GetComponentInChildren][SerializeField] ClientsCap clientsCap;
    [GetComponentInChildren][SerializeField] ClientsPerDay clientsPerDay;
    [GetComponentInChildren][SerializeField] RevenuePerClient revenuePerClient;
    [GetComponentInChildren][SerializeField] StoreCleanness storeCleanness;
    [GetComponentInChildren][SerializeField] Revenue revenue;
    [FormerlySerializedAs("costs")] [GetComponentInChildren][SerializeField] WorkersSalary workersSalary;
    [GetComponentInChildren][SerializeField] Rent rent; 
    [GetComponentInChildren][SerializeField] Costs costs;

    [Required] [SerializeField] SummaryUI summaryUI;

    public DaySummary CalculateSummary(Store store)
    {
        List<IStoreObject> shopObjects = store.GetStoreObjects();
        Workers workers = store.GetWorkers();
        var storeCleannessValue = storeCleanness.Recalculate(shopObjects);
        var attractiveness = storeAttractiveness.Recalculate(shopObjects, storeCleannessValue);
        var clientsCapValue = clientsCap.Recalculate(shopObjects);
        var clients = clientsPerDay.Recalculate(attractiveness, clientsCapValue); 
        var revenuePerClientValue = revenuePerClient.Recalculate(shopObjects); 
        var revenueValue = revenue.Recalculate(revenuePerClientValue, clients);
        var workersSalaryValue = workersSalary.Recalculate(workers);
        var rentValue = rent.Recalculate();
        var costsValue = costs.Recalculate(workersSalaryValue, rentValue);
        var income = revenueValue - costsValue;
        var summary = new DaySummary{
            income = income,
            customersServed = clients,
            revenue = revenueValue,
            averageSpending = revenuePerClientValue,
            workersSalary = workersSalaryValue,
            rent = rentValue,
            costs = costsValue
        };
        summaryUI.ShowSummary(summary);
        return summary;
    }
}

public struct DaySummary
{
    public float income;
    public float customersServed;
    public float revenue;
    public float averageSpending;
    public float workersSalary;
    public float rent;
    public float costs;
}