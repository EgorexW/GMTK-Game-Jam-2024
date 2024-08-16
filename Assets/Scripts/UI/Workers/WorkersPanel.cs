using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Events;

public class WorkersPanel : MonoBehaviour
{
    List<WorkerTypePanel> panels;

    [Foldout("Event")] public UnityEvent<Worker> onClickEvent = new ();
    void Awake()
    {
        panels = new List<WorkerTypePanel>(GetComponentsInChildren<WorkerTypePanel>());
        foreach (var panel in panels){
            panel.onClickEvent.AddListener(onClickEvent.Invoke);
        }
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowWorkers(Workers workers)
    {
        Show();
        foreach (var panel in panels){
            panel.SetupPanel(workers.GetWorkersOfType(panel.GetWorkerType()));
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}