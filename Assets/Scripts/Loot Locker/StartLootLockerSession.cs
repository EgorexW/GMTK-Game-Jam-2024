using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class StartLootLockerSession : MonoBehaviour
{
    void Awake(){
        StartCoroutine(LoginRoutine());
    }

    IEnumerator LoginRoutine(){
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) => {
            if (response.success){
                Debug.Log("Logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                PlayerPrefs.Save();
            } else {
                Debug.Log($"Failed to login: {response.statusCode}");
            }
            done = true;
        });
        yield return new WaitWhile(() => !done);
    }
}
