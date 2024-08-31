using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class InfrastructureClick : MonoBehaviour
{
    [Foldout("Events")] public UnityEvent<InfrastructureObject> onInfrastructureClick = new ();
    
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (General.IsMouseOverUI()) return;
        var pos = General.GetMousePos();
        var target = Physics2D.OverlapPoint(pos);
        var infrastructure = General.GetComponentFromCollider<InfrastructureObject>(target);
        onInfrastructureClick.Invoke(infrastructure);
    }
}
