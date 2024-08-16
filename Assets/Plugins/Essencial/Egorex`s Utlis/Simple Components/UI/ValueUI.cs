using NaughtyAttributes;
using UnityEngine;

public class ValueUI : TextUI
{
    [SerializeField][Required] Value value;

    protected override void Awake()
    {
        base.Awake();
        value.OnValueChanged.AddListener(OnValueChanged);
        UpdateUI(value.GetValue());
    }

    void OnValueChanged(ValueChangeCallback valueChangeCallback)
    {
        UpdateUI(valueChangeCallback.newValue);
    }
}
