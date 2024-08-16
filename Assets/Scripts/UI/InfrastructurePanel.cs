using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class InfrastructurePanel : MonoBehaviour
{
    [Required] [SerializeField] TextMeshProUGUI titleText;
    [Required] [SerializeField] TextUI sellText;
    [Required][SerializeField] TextMeshProUGUI descriptionText;
    
    public void Show(InfrastructureObject infrastructure)
    {
        Show();
        var infrastructureType = infrastructure.GetInfrastructureType();
        titleText.text = infrastructureType.name;
        sellText.UpdateUI(infrastructureType.sellValue);
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
