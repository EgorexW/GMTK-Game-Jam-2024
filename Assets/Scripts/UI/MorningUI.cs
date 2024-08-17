using UnityEngine;

class MorningUI : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}