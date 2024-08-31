using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PaidActionUI : MonoBehaviour
{
    [Required][SerializeField] TextUI buyCostText;
    [Required][SerializeField] TextMeshProUGUI nameText;
    [Required][SerializeField] TextMeshProUGUI descriptionText;
    
    [Foldout("Events")] public UnityEvent<PaidAction> onClickEvent = new ();
    
    PaidAction paidAction;

    public void Show(PaidAction action)
    {
        paidAction = action;
        buyCostText.UpdateUI(action.GetCost());
        nameText.text = action.name;
        descriptionText.text = action.description;
    }
    
    public void OnClick()
    {
        onClickEvent.Invoke(paidAction);
    }
}