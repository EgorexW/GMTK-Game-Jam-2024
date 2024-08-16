using UnityEngine;
using UnityEngine.SceneManagement;

public class OnKeyDownRestart : KeyDownEvent
{
    [SerializeField] protected bool async;
    public void Restart()
    {
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (async){
            SceneManager.LoadSceneAsync(sceneBuildIndex);
        } else {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
    void Awake()
    {
        onKeyDown.AddListener(Restart);
    }
}