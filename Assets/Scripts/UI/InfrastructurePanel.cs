using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfrastructurePanel : MonoBehaviour
{
    const string CANT_SELL_TEXT = "Can't sell";
    
    [Required] [SerializeField] TextMeshProUGUI titleText;
    [Required] [SerializeField] TextUI sellText;
    [Required] [SerializeField] TextMeshProUGUI descriptionText;
    [Required] [SerializeField] Button button;
    
    [Foldout("Event")] public UnityEvent<InfrastructureObject> onClickEvent;
    
    InfrastructureObject infrastructure;

    void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    void Start()
    {
        Hide();
    }

    void OnClick()
    {
        onClickEvent.Invoke(infrastructure);
    }

    public void Show(InfrastructureObject infrastructureTmp)
    {
        Show();
        infrastructure = infrastructureTmp;
        var infrastructureType = infrastructure.GetInfrastructureType();
        titleText.text = infrastructureType.name;
        if (!infrastructureType.sellValue){
            button.interactable = false;
            sellText.UpdateUI(CANT_SELL_TEXT);
        }
        else{
            sellText.UpdateUI(infrastructureType.sellValue);
        }
        descriptionText.text = infrastructureType.description;
    }
    void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
