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
    [GetComponentInChildren][SerializeField] InfrastructurePanelLink infrastructurePanelLink;

    [Foldout("Events")] public UnityEvent onEndMorning = new UnityEvent();
    
    public void BeginMorning(Store store)
    {
        morningUI.Show();
        workerPanelLink.SetWorkers(store.GetWorkers());
        infrastructurePanelLink.SetInfrastructure(store.GetInfrastructure());
    }

    public void EndMorning()
    {
        morningUI.Hide();
        onEndMorning.Invoke();
    }
}