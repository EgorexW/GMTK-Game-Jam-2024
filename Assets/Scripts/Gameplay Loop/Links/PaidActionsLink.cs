using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaidActionsLink : MonoBehaviour
{
    [SerializeField] PaidActionsUI paidActionsUI;
    PaidActions paidActions;

    void Awake()
    {
        paidActionsUI.onClickEvent.AddListener(OnActionSelected);
    }

    public void SetPaidActions(PaidActions newPaidActions)
    {
        paidActions = newPaidActions;
    }

    public void Show()
    {
        paidActionsUI.Show(paidActions);
    }

    public void OnActionSelected(PaidAction action)
    {
        paidActions.BuyAction(action);
        Show();
    }
}
