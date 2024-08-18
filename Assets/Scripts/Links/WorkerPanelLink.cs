using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class WorkerPanelLink : MonoBehaviour
{
    [Required] [SerializeField] WorkersPanel workersPanel;
    
    Workers workers;

    public void SetWorkers(Workers newWorkers)
    {
        workers = newWorkers;
    }
    public void ShowWorkerPanel()
    {
        workersPanel.ShowWorkers(workers);
    }
    public void OnWorkerClicked(Worker worker)
    {
        workers.FireWorker(worker);
        ShowWorkerPanel();
    }
}
