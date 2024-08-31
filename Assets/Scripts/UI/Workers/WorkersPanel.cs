using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class WorkersPanel : MonoBehaviour
{    [Foldout("Events")] public UnityEvent<Worker> onClickEvent = new ();
     void Awake()
     {
         objectsFactory.onCreateObject.AddListener(OnCreateObject);
     }
 
     void OnCreateObject(GameObject arg0)
     {
         var panel = arg0.GetComponent<WorkerTypePanel>();
         panel.onClickEvent.AddListener(onClickEvent.Invoke);
     }
 
     void Start()
     {
         Hide();
     }
    [Required] [SerializeField] ObjectsFactory objectsFactory;
    


    public void ShowWorkers(Workers workers)
    {
        Show();
        var workerTypes = workers.GetWorkersByType();
        objectsFactory.SetCount(workerTypes.Count);
        var panels = objectsFactory.GetObjects();
        var types = new List<WorkerType>(workerTypes.Keys);
        for (int i = 0; i < panels.Count; i++)
        {
            var panel = panels[i].GetComponent<WorkerTypePanel>();
            var workerType = types[i];
            panel.SetupPanel(workerTypes[workerType], workerType);
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