using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class WorkerPanelLink : MonoBehaviour
{
    [Required] [SerializeField] Workers workers;
    [Required] [SerializeField] WorkersPanel workersPanel;

    [Button]
    void ShowWorkerPanel()
    {
        workersPanel.ShowWorkers(workers);
    }
    public void OnWorkerClicked(Worker worker)
    {
        workers.FireWorker(worker);
        ShowWorkerPanel();
    }
}
