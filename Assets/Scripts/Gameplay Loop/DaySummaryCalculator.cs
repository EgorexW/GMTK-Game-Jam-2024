using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class DaySummaryCalculator : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] StoreAttractiveness storeAttractiveness;
    [GetComponentInChildren][SerializeField] ClientsCap clientsCap;
    [GetComponentInChildren][SerializeField] ClientsPerDay clientsPerDay;
    [GetComponentInChildren][SerializeField] RevenuePerClient revenuePerClient;
    [GetComponentInChildren][SerializeField] DirtGenerator dirtGenerator;
    [GetComponentInChildren][SerializeField] WorkersSalary workersSalary;

    public DaySummary CalculateSummary(Store store)
    {
        List<IStoreObject> shopObjects = store.GetStoreObjects();
        Workers workers = store.GetWorkers();
        var attractiveness = storeAttractiveness.Recalculate(shopObjects);
        var customersServedCap = clientsCap.Recalculate(shopObjects);
        var totalCustomers = clientsPerDay.Recalculate(attractiveness, store.GetDirt().GetValue());
        var customersServed = Mathf.Min(totalCustomers, customersServedCap);
        var dirtGenerated = dirtGenerator.Recalculate(shopObjects, customersServed);
        var revenuePerClientValue = revenuePerClient.Recalculate(shopObjects);
        var revenueValue = revenuePerClientValue * customersServed;
        var workersSalaryValue = workersSalary.Recalculate(workers);
        var income = revenueValue - workersSalaryValue;
        var summary = new DaySummary{
            income = income,
            totalCustomers = totalCustomers,
            customersServed = customersServed,
            revenue = revenueValue,
            averageSpending = revenuePerClientValue,
            workersSalary = workersSalaryValue,
            dirtGenerated = dirtGenerated,
            costs = workersSalaryValue
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
    public float dirtGenerated;
    public float costs;
    public int totalCustomers;
}