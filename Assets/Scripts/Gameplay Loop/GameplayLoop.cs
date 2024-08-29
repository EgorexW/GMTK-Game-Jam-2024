using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Events;

public class GameplayLoop : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] Day day;
    [GetComponentInChildren][SerializeField] Morning morning;
    
    [Required][SerializeField] Store store;
    
    DaySummary lastSummary;
    bool running;

    [Foldout("Events")] public UnityEvent<DaySummary> onDayEnd;
    [Foldout("Events")] public UnityEvent<CloseStoreSummary> onCloseStore;
    [Foldout("Events")] public UnityEvent onRunDay;

    void Awake()
    {
        store.onCloseStore.AddListener(End);
        morning.onEndMorning.AddListener(OnMorningEnd);
        day.onDayEnd.AddListener(OnDayEnd);
    }
    void Start()
    {
        running = true;
        BeginMorning();
    }

    void End(CloseStoreSummary summary)
    {
        onCloseStore.Invoke(summary);
        running = false;
    }
    void BeginMorning()
    {
        if (!running) return; 
        morning.BeginMorning(store);
    }
    void OnMorningEnd()
    {
        RunDay();
    }

    public void RunDay()
    {
        if (!running) return;
        onRunDay.Invoke();
        lastSummary = store.GetDaySummaryCalculator().CalculateSummary(store);
        day.RunDay(store, lastSummary);
    }

    void OnDayEnd()
    {
        EndDay();
    }

    void EndDay()
    {
        if (!running) return;
        store.EndDay(lastSummary);
        onDayEnd.Invoke(lastSummary);
        BeginMorning();
    }
}
