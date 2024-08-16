using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconsUI : CountUI
{
    List<GameObject> activeIconUIs = new();
    Queue<GameObject> inactiveIconUIs = new();
    GameObject prefab;
    [SerializeField] Sprite defaultSprite;
    

    // [Foldout("Events")][SerializeField] UnityEvent onShow;
    // [Foldout("Events")][SerializeField] UnityEvent onHide;
    void Awake(){
        prefab = transform.GetChild(0).gameObject;
        prefab.SetActive(false);
        if (defaultSprite == null){
            defaultSprite = prefab.GetComponentInChildren<Image>().sprite;
        }
    }
    public override void UpdateUI(int count)
    {
        SetCount(count);
    }

    void SetCount(int count)
    {
        while(activeIconUIs.Count > count){
            RemoveIcon();
        }
        while(activeIconUIs.Count < count){
            AddIcon();
        }
    }

    void AddIcon()
    {
        AddIcon(defaultSprite);
    }

    #region Show/Hide

    public void Expand(){
        Show();
    }
    public void Collapse(){
        Hide();
    }
    public void Hide(){
        // onHide.Invoke();
        gameObject.SetActive(false);
    }
    public void Show(){
        // onShow.Invoke();
        gameObject.SetActive(true);
    }

    #endregion
    public void AddIcon(Sprite sprite){
        if (inactiveIconUIs.Count < 1){
            CreateIconUI();
        }
        GameObject IconUI = inactiveIconUIs.Dequeue();
        IconUI.SetActive(true);
        IconUI.GetComponentInChildren<Image>().sprite = sprite;
        activeIconUIs.Add(IconUI);
    }

    public void RemoveIcon(Sprite sprite = null){
        var iconUI = activeIconUIs[^1];
        if (sprite != null){
            iconUI = activeIconUIs.Find(x => x.GetComponentInChildren<Image>().sprite == sprite);
            if (iconUI == null){
                return;
            }
        }
        iconUI.SetActive(false);
        activeIconUIs.Remove(iconUI);
        inactiveIconUIs.Enqueue(iconUI);
    }
    public void ClearIcons(){
        SetCount(0);
    }

    void CreateIconUI()
    {
        inactiveIconUIs.Enqueue(
            Instantiate(prefab, transform));
    }
}
