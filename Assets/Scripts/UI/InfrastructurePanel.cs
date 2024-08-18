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
    
    [Foldout("Events")] public UnityEvent<InfrastructureObject> onClickEvent;
    
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
        if (infrastructureTmp == null){
            Hide();
            return;
        }
        Show();
        infrastructure = infrastructureTmp;
        var infrastructureType = infrastructure.GetInfrastructureType();
        titleText.text = infrastructureType.name;
        if (!infrastructure.GetSellValue()){
            button.interactable = false;
            sellText.UpdateUI(CANT_SELL_TEXT);
        }
        else{
            button.interactable = true;
            sellText.UpdateUI(infrastructure.GetSellValue());
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
