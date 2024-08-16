using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WorkerTypePanel : MonoBehaviour
{
    [SerializeField][Required] WorkerType workerType;

    [SerializeField][Required] ObjectsUI workersCountUI;
    [SerializeField][Required] TextMeshProUGUI workersTitle;
    
    [Foldout("Event")] public UnityEvent<Worker> onClickEvent = new ();

    public WorkerType GetWorkerType()
    {
        return workerType;
    }

    public void SetupPanel(List<Worker> workers)
    {
        OnValidate();
        workersCountUI.UpdateUI(workers.Count);
        var workerUIs = workersCountUI.GetActiveObjs();
        for (int i = 0; i < workers.Count; i++){
            var worker = workers[i];
            var workerUI = workerUIs[i].GetComponent<WorkerUI>();
            workerUI.Setup(worker);
        }
    }

    public void OnClick(Worker worker)
    {
        onClickEvent.Invoke(worker);
    }

    void OnValidate()
    {
        workersTitle.text = workerType.name;
    }
}