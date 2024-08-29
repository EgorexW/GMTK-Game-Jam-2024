using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class SummaryUI : MonoBehaviour
{
    [Required][SerializeField] TextUI customersServedUI;
    [Required][SerializeField] TextUI averageSpendingUI;
    [Required][SerializeField] TextUI workersSalaryUI;
    // [Required] [SerializeField] TextUI rentUI;
    [Required][SerializeField] TextUI revenueUI;
    [Required][SerializeField] TextUI costsUI;
    [Required][SerializeField] TextUI incomeUI;
    
    [Required][SerializeField] TextUI topBarIncomeUI;

    [Foldout("Events")] public UnityEvent onShow;

    void Start()
    {
        Hide();
    }

    public void ShowSummary(DaySummary summary)
    {
        Show();
        customersServedUI.UpdateUI(summary.customersServed);
        averageSpendingUI.UpdateUI(summary.averageSpending);
        workersSalaryUI.UpdateUI(summary.workersSalary);
        // rentUI.UpdateUI(summary.rent);
        revenueUI.UpdateUI(summary.revenue);
        costsUI.UpdateUI(summary.costs);
        incomeUI.UpdateUI(summary.income);
        topBarIncomeUI.UpdateUI(summary.income);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        onShow.Invoke();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
