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
    [FormerlySerializedAs("dirtynnessGenerated")] [FormerlySerializedAs("storeCleanness")] [GetComponentInChildren][SerializeField] DirtGenerated dirtGenerated;
    [FormerlySerializedAs("costs")] [GetComponentInChildren][SerializeField] WorkersSalary workersSalary;
    
    [SerializeField] float rent = 150;

    public DaySummary CalculateSummary(Store store)
    {
        List<IStoreObject> shopObjects = store.GetStoreObjects();
        Workers workers = store.GetWorkers();
        var attractiveness = storeAttractiveness.Recalculate(shopObjects);
        var customersServedCap = clientsCap.Recalculate(shopObjects);
        var totalCustomers = clientsPerDay.Recalculate(attractiveness);
        var customersServed = Mathf.Min(totalCustomers, customersServedCap);
        var storeCleannessValue = dirtGenerated.Recalculate(shopObjects, customersServed);
        var revenuePerClientValue = revenuePerClient.Recalculate(shopObjects);
        var revenueValue = revenuePerClientValue * customersServed;
        var workersSalaryValue = workersSalary.Recalculate(workers);
        var costsValue = rent + workersSalaryValue;
        var income = revenueValue - costsValue;
        var summary = new DaySummary{
            income = income,
            totalCustomers = totalCustomers,
            customersServed = customersServed,
            revenue = revenueValue,
            averageSpending = revenuePerClientValue,
            workersSalary = workersSalaryValue,
            rent = rent,
            costs = costsValue
        };
        return summary;
    }

    public void DayPassed()
    {
        clientsPerDay.DayPassed();
    }
}

public struct DaySummary
{
    public float income;
    public int customersServed;
    public float revenue;
    public float averageSpending;
    public float workersSalary;
    public float rent;
    public float costs;
    public int totalCustomers;
}