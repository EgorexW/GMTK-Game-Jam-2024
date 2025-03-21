using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class RestartButton : MonoBehaviour
{
    [SerializeField] protected bool async = false;
    public void Restart()
    {
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (async){
            SceneManager.LoadSceneAsync(sceneBuildIndex);
        } else {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
#if UNITY_EDITOR
    void Reset()
    {
        if (!TryGetComponent<Button>(out Button button)){
            return;
        }
        for (int i = 0; i < button.onClick.GetPersistentEventCount(); i++){
            if (button.onClick.GetPersistentMethodName(i) == "Restart"){
                return;
            }
        }
        UnityEventTools.AddPersistentListener(button.onClick, Restart);
    }
#endif
}
