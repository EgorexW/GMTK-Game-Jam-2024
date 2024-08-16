using UnityEngine;

public class OnKeyDownQuit : KeyDownEvent
{
    void Awake()
    {
        onKeyDown.AddListener(CloseGame);
    }

    public static void CloseGame(){
        Application.Quit();
    }
}