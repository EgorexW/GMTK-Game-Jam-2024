using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkerUI : MonoBehaviour
{
    [Required] [SerializeField] Image image;
    [Required] [SerializeField] TextUI textUI;
    
    public void Setup(Worker worker)
    {
        image.sprite = worker.GetWorkerType().sprite;
        if (worker.GetDaysTillFire()){
            textUI.UpdateUI(worker.GetDaysTillFire());
        }
        else{
            textUI.UpdateUI("");
        }
    }
}