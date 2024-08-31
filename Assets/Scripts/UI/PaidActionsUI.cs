using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PaidActionsUI : MonoBehaviour
{
    [GetComponentInChildren] [SerializeField]
    ObjectsUI objectsUI;
    
    [Foldout("Events")] public UnityEvent<PaidAction> onClickEvent = new ();
    void Awake()
    {
        objectsUI.onCreateObject.AddListener(OnCreateObject);
    }

    void OnCreateObject(GameObject arg0)
    {
        var panel = arg0.GetComponent<PaidActionUI>();
        panel.onClickEvent.AddListener(onClickEvent.Invoke);
    }

    void Start()
    {
        Hide();
    }
    public void Show(PaidActions paidActions)
    {
        gameObject.SetActive(true);
        var actions = paidActions.GetActions();
        objectsUI.UpdateUI(actions.Count);
        var objects = objectsUI.GetActiveObjs();
        for (var i = 0; i < actions.Count; i++){
            var action = actions[i];
            var obj = objects[i];
            obj.GetComponent<PaidActionUI>().Show(action);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}