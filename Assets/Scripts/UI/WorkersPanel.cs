using System;
using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class WorkersPanel : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowWorkers(Workers workers)
    {
        Show();
        var panels = GetComponentsInChildren<WorkerTypePanel>();
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