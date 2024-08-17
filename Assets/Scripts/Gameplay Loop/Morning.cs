using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Morning : MonoBehaviour
{
    [Required] [SerializeField] MorningUI morningUI;

    [Foldout("Events")] public UnityEvent onEndMorning = new UnityEvent();

    public void BeginMorning()
    {
        morningUI.Show();
    }

    public void EndMorning()
    {
        morningUI.Hide();
        onEndMorning.Invoke();
    }
}