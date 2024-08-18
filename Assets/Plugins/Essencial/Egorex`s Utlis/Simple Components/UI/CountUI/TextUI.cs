using System;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.Events;

#if UNITY_EDITOR
#endif

[DefaultExecutionOrder(order: -100)]
public class TextUI : CountUI
{
    const string STR_TO_SUBSTITUTE = "{nr}";

    [SerializeField] protected Optional<float> startValue = new(0, false);
    [SerializeField] float zeros = -1;
    [SerializeField] float valueModifier = 0;
    [InfoBox( STR_TO_SUBSTITUTE + " is substituted with the value")]
    [SerializeField][Required] TextMeshProUGUI text;

    string format;

    protected virtual void Awake()
    {
        format = text.text;
        if (!format.Contains(STR_TO_SUBSTITUTE)){
            Debug.LogWarning("TextUI: Text does not contain " + STR_TO_SUBSTITUTE, this);
            format = STR_TO_SUBSTITUTE;
        }
        if (startValue){
            UpdateUI(startValue);
        }
    }
    public override void UpdateUI(float count)
    {
        count += valueModifier;
        count = Mathf.Round(count * Mathf.Pow(10, -zeros)) / Mathf.Pow(10, -zeros);
        var textToShow = format.Replace(STR_TO_SUBSTITUTE, count.ToString());
        onUpdate.Invoke(count);
        UpdateUI(textToShow);
    }
    public void UpdateUI(string textToShow){
        text.text = textToShow;
    }

    public override void UpdateUI(int count)
    {
        UpdateUI((float)count);
    }

    void Reset()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
}
