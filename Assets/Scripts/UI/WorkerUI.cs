using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WorkerUI : MonoBehaviour
{
    [Required] [SerializeField] Image image;
    [Required] [SerializeField] TextUI textUI;
    [Required] [SerializeField] Button button;
    
    [Foldout("Event")] public UnityEvent<Worker> onClickEvent = new ();
    
    Worker worker;


    void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        onClickEvent.Invoke(worker);
    }

    public void Setup(Worker workerTmp)
    {
        worker = workerTmp;
        image.sprite = worker.GetWorkerType().sprite;
        if (worker.GetDaysTillFire()){
            textUI.UpdateUI(worker.GetDaysTillFire());
            button.interactable = false;
        }
        else{
            textUI.UpdateUI("");
        }
    }
}