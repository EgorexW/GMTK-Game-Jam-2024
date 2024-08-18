using System;
using NaughtyAttributes;
using UnityEngine;

public class StoreClosedUI : MonoBehaviour
{
    [Required][SerializeField] TextUI moneyUI;
    [Required][SerializeField] TextUI infrastructureSoldUI;
    [Required][SerializeField] TextUI moneyMultiplierUI;
    [Required] [SerializeField] TextUI overallMoneyUI;
    
    void Awake()
    {
        Hide();
    }

    public void ShowSummary(CloseStoreSummary summary)
    {
        moneyUI.UpdateUI(summary.money);
        infrastructureSoldUI.UpdateUI(summary.infrastructureSold);
        moneyMultiplierUI.UpdateUI(summary.moneyMultiplier);
        overallMoneyUI.UpdateUI(summary.overallMoney);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}