using Nrjwolf.Tools.AttachAttributes;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextUI))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class IncomeColor : MonoBehaviour
{
    [SerializeField][GetComponent] TextMeshProUGUI text;
    [SerializeField][GetComponent] TextUI textUI;

    void Awake()
    {
        textUI.onUpdate.AddListener(UpdateColor);
    }

    public void UpdateColor(float arg0)
    {
        text.color = GetColor(arg0);
    }

    static Color GetColor(float income)
    {
        switch (income){
            case < 0:
                return Color.red;
            case > 0:
                return Color.green;
            default:
                return Color.white;
        }
    }
}
