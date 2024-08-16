using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public interface IValue
{
    UnityEvent<ValueChangeCallback> OnValueChanged{ get; }
    float GetValue();
    Vector2 GetMinMax();
    void SetValue(float newValue);
}

public struct ValueChangeCallback
{
    public float newValue;
    public float previousValue;
    public float diff => newValue - previousValue;

    public ValueChangeCallback(float value, float previousValueTmp)
    {
        newValue = value;
        previousValue = previousValueTmp;
    }
}

public class Value : MonoBehaviour, IValue
{
    public bool log;
    
    [SerializeField] float value;
    [SerializeField] Vector2 minMax = new Vector2(0, 100);
    
    public UnityEvent<ValueChangeCallback> OnValueChanged => onValueChanged;

    [field: Foldout("Events")] public UnityEvent<ValueChangeCallback> onValueChanged = new();

    public float GetValue()
    {
        return value;
    }

    public Vector2 GetMinMax()
    {
        return minMax;
    }

    public void SetValue(float newValue)
    {
        if (log){
            Debug.Log($"Setting value to {newValue}", this);
        }
        var previousValue = value;
        value = Mathf.Clamp(newValue, minMax.x, minMax.y);
        onValueChanged.Invoke(new(value, previousValue));
    }
}

public static class ValueExtentions
{
    public static void ModifyValue(this Value value, float mod)
    {
        if (value.log){
            Debug.Log("Modifing value by " + (mod), value);
        }
        value.SetValue(value.GetValue() + mod);
    }

    public static void MultiplyValue(this Value value, float mod)
    {
        if (value.log){
            Debug.Log("Multipling value by " + (mod), value);
        }
        value.SetValue(value.GetValue() * mod);
    }
    public static bool HasValue(this Value value, int requiredValue)
    {
        return value.GetValue() >= requiredValue;
    }
}
