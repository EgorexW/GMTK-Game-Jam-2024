using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
#endif

public abstract class CountUI : MonoBehaviour
{
    [Foldout("Events")] public UnityEvent<float> onUpdate;

    public virtual void UpdateUI(int count)
    {
        onUpdate.Invoke(count);
    }
    public virtual void UpdateUI(float count){
        UpdateUI(Mathf.RoundToInt(count));
    }
}