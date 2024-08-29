using System;
using NaughtyAttributes;
using UnityEngine;

public class StoreClosedUI : MonoBehaviour
{
    [Required][SerializeField] TextUI moneyUI;
    [Required][SerializeField] TextUI moneyMultiplierUI;
    [Required] [SerializeField] TextUI overallMoneyUI;
    [Required] [SerializeField] ShowLeaderboard showLeaderboard;
    
    void Awake()
    {
        Hide();
    }

    public void ShowSummary(CloseStoreSummary summary)
    {
        Show();
        moneyUI.UpdateUI(summary.money);
        moneyMultiplierUI.UpdateUI(summary.moneyMultiplier);
        overallMoneyUI.UpdateUI(summary.overallMoney);
        showLeaderboard.Show();
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