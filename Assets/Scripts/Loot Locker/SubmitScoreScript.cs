using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class SubmitScoreScript : MonoBehaviour
{
    public void SubmitScore(CloseStoreSummary closeStoreSummary)
    {
        SubmitScore(closeStoreSummary.overallMoney);
    }
    public void SubmitScore(float score){
        StartCoroutine(SubmitScoreRoutine((int)(score*100)));
    }
    IEnumerator SubmitScoreRoutine(int score){
        bool done = false;
        string leaderboardID = SceneManager.GetActiveScene().name.Replace(" ", "");
        LootLockerSDKManager.SubmitScore(PlayerPrefs.GetString("PlayerID"), score, leaderboardID, (response) => {
            if (response.success){
                Debug.Log("Score submitted");
            } else {
                Debug.Log("Failed to submit score");
            }
            done = true;
        });
        yield return new WaitWhile(() => !done);
    }
}
