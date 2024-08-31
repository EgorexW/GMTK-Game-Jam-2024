using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaidActions : MonoBehaviour
{
    List<PaidAction> actions;
    Store store;

    void Awake()
    {
        actions = new List<PaidAction>(GetComponentsInChildren<PaidAction>());
        store = General.GetRootComponent<Store>(transform);
    }

    public List<PaidAction> GetActions()
    {
        return actions.Copy();
    }

    public void BuyAction(PaidAction action)
    {
        if (action.activatedThisDay){
            return;
        }
        if (store.GetMoney().GetValue() < action.GetCost()){
            return;
        }
        action.activatedThisDay = true;
        store.GetMoney().ModifyValue(-action.Buy(store));
        action.IncreaseCost();
    }
}