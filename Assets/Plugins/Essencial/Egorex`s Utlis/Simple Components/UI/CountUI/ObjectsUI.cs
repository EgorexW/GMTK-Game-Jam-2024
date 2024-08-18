using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class ObjectsUI : CountUI
{
    [SerializeField] GameObject prefab = null;
    
    List<GameObject> activeObjs = new();
    Queue<GameObject> inactiveObjs = new();

    [Foldout("Events")] public UnityEvent<GameObject> onCreateObject = new();
    

    // [Foldout("Events")][SerializeField] UnityEvent onShow;
    // [Foldout("Events")][SerializeField] UnityEvent onHide;
    void Awake()
    {
        SetPrefab();
        prefab.SetActive(false);
    }

    void SetPrefab()
    {
        if (prefab != null) return;
        if (transform.childCount <= 0) return;
        prefab = transform.GetChild(0).gameObject;
    }

    public override void UpdateUI(int count)
    {
        base.UpdateUI(count);
        SetCount(count);
    }

    void SetCount(int count)
    {
        while(activeObjs.Count > count){
            RemoveObject();
        }
        while(activeObjs.Count < count){
            AddObject();
        }
    }

    public GameObject AddObject()
    {
        if (inactiveObjs.Count < 1){
            CreateIconUI();
        }
        GameObject obj = inactiveObjs.Dequeue();
        obj.SetActive(true);
        activeObjs.Add(obj);
        return obj;
    }
    
    public void RemoveObject(GameObject obj = null){
        var iconUI = activeObjs[^1];
        if (obj == null){
            obj = activeObjs[^1];
        }
        if (obj == null || !activeObjs.Contains(obj)){
            return;
        }
        obj.SetActive(false);
        activeObjs.Remove(obj);
        inactiveObjs.Enqueue(obj);
    }
    public void ClearIcons(){
        SetCount(0);
    }

    public List<GameObject> GetActiveObjs()
    {
        return new(activeObjs);
    }
    void CreateIconUI()
    {
        var newObj = Instantiate(prefab, transform);
        inactiveObjs.Enqueue(newObj);
        onCreateObject.Invoke(newObj);
    }

    void OnValidate()
    {
        SetPrefab();
    }
    
    #region Show/Hide
    public void Hide(){
        // onHide.Invoke();
        gameObject.SetActive(false);
    }
    public void Show(){
        // onShow.Invoke();
        gameObject.SetActive(true);
    }

    #endregion
}
