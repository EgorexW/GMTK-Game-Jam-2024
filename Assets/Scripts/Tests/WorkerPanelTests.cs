using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class WorkerPanelTests : MonoBehaviour
{
    [Required] [SerializeField] Workers workers;
    [Required] [SerializeField] WorkersPanel workersPanel;

    [Button]
    void TestWorkerPanel()
    {
        workersPanel.ShowWorkers(workers);
    }
}
