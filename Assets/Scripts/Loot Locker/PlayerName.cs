using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Events;

public class PlayerName : MonoBehaviour
{
    public UnityEvent onComplete;
    
    public void SetPlayerName(string name){
        StartCoroutine(SetPlayerNameRoutine(name));
    }
    
    IEnumerator SetPlayerNameRoutine(string name){
        bool done = false;
        LootLockerSDKManager.SetPlayerName(name, (response) => {
            if (response.success){
                Debug.Log("Name set");
                PlayerPrefs.SetString("PlayerName", name);
            } else {
                Debug.Log("Failed to set name");
            }
            done = true;
        });
        yield return new WaitWhile(() => !done);
        onComplete.Invoke();
    }
    
    public static string GetPlayerID()
    {
        return PlayerPrefs.GetString("PlayerID", "");
    }
}
