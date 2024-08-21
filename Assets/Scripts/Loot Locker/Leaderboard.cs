using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;


public class Leaderboard : MonoBehaviour
{
    [SerializeField] string LeaderboardID = "10830";
    [SerializeField] TMP_Text playerNamesText;
    [SerializeField] TMP_Text playerScoresText;
    [SerializeField] bool getSurroundingScores = false;
    [SerializeField] bool fetchOnStart = true;
    [SerializeField] int nr;
    void Start(){
        playerNamesText.text = "Loading..";
        playerScoresText.text = "";
        if (fetchOnStart){
            FetchTopHighScores();
        }
    }
    public void FetchTopHighScores(){
        StartCoroutine(FetchTopHighScoresRoutine());
    }
    public void FetchTopHighScores(string LeaderboardID){
        this.LeaderboardID = LeaderboardID;
        // Debug.Log(LeaderboardID);
        FetchTopHighScores();
    }
    IEnumerator FetchTopHighScoresRoutine(){
        bool done = false;
        if (getSurroundingScores){
            yield return GetSurroundingScores();
            yield break;
        }
        LootLockerSDKManager.GetScoreList(LeaderboardID, nr, 0, (response) => {
            if (response.success){
                string playerNames = "";
                string playerScores = "";
                LootLockerLeaderboardMember[] members = response.items;
                foreach (LootLockerLeaderboardMember member in members){
                    playerNames += member.rank + ". ";
                    if (member.player.id.ToString() == PlayerPrefs.GetString("PlayerID", "")){
                        if (member.player.name != "")
                            playerNames += "<color=red>" + member.player.name + "</color>";
                        else
                            playerNames += "<color=red>You</color>";
                    }
                    else if (member.player.name != ""){
                        playerNames += member.player.name;
                    } else {
                        playerNames += "Guest";
                    }
                    string score = member.score.ToString();
                    playerScores += score + "\n";
                    playerNames += "\n";
                }
                playerNamesText.text = playerNames;
                playerScoresText.text = playerScores;
            } else {
                Debug.Log("Failed to get top scores");
            }
            done = true;
        });
        yield return new WaitWhile(() => !done);
    }

    IEnumerator GetSurroundingScores(){
        yield return new WaitForSeconds(1);
        bool done = false;
        LootLockerSDKManager.GetMemberRank(LeaderboardID, PlayerPrefs.GetString("PlayerID", ""), (response) =>
        {
            if (response.statusCode == 200)
            {
                int rank = response.rank;
                int count = nr;
                int after = rank < nr ? 0 : rank - Mathf.CeilToInt(nr/2f);

                LootLockerSDKManager.GetScoreList(LeaderboardID, count, after, (response) =>
                {
                    if (response.statusCode == 200)
                    {
                        Debug.Log("Successful");
                        string playerNames = "";
                        string playerScores = "";
                        LootLockerLeaderboardMember[] members = response.items;
                        foreach (LootLockerLeaderboardMember member in members)
                        {
                            playerNames += member.rank + ". ";
                            if (member.player.id.ToString() == PlayerPrefs.GetString("PlayerID", "")){
                                if (member.player.name != "")
                                    playerNames += "<color=red>" + member.player.name + "</color>";
                                else
                                    playerNames += "<color=red>You</color>";
                            }
                            else if (member.player.name != "")
                            {
                                playerNames += member.player.name;
                            }
                            else
                            {
                                playerNames += "Guest";
                            }
                            string score = member.score.ToString();
                            playerScores += score + "\n";
                            playerNames += "\n";
                        }
                        playerNamesText.text = playerNames;
                        playerScoresText.text = playerScores;
                    }
                    else
                    {
                        Debug.Log("failed: " + response.Error);
                    }
                });
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
            done = true;
        });
        yield return new WaitWhile(() => !done);
    }
}
