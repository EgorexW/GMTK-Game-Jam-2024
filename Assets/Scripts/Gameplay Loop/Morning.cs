using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Morning : MonoBehaviour
{
    [Required] [SerializeField] MorningUI morningUI;
    [GetComponentInChildren][SerializeField] WorkerPanelLink workerPanelLink;

    [Foldout("Events")] public UnityEvent onEndMorning = new UnityEvent();
    
    public void BeginMorning(Store store)
    {
        morningUI.Show();
        workerPanelLink.SetWorkers(store.GetWorkers());
    }

    public void EndMorning()
    {
        morningUI.Hide();
        onEndMorning.Invoke();
    }
}