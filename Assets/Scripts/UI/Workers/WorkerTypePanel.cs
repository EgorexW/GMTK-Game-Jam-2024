using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WorkerTypePanel : MonoBehaviour
{
    [SerializeField][Required] TextMeshProUGUI workersTitle;
    [Required][SerializeField] TextMeshProUGUI description;
    [Required] [SerializeField] CountUI salary;
    [SerializeField][Required] ObjectsUI workersCountUI;
    
    [Foldout("Events")] public UnityEvent<Worker> onClickEvent = new ();
    
    public void SetupPanel(List<Worker> workers, WorkerType workerType)
    {
        workersTitle.text = workerType.name;
        description.text = workerType.description;
        salary.UpdateUI(workerType.salary);
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
}