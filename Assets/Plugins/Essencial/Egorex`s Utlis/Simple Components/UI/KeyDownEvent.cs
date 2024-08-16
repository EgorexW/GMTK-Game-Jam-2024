using UnityEngine;
using UnityEngine.Events;

public class KeyDownEvent : MonoBehaviour
{
    public KeyCode actionKey = KeyCode.Escape;
    public UnityEvent onKeyDown = new();

    void Update()
    { 
        if (Input.GetKeyDown(actionKey))
        {
            onKeyDown.Invoke();
        }
    }
}
