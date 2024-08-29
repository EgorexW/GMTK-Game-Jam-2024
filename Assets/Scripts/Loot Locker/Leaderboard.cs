using System;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Serialization;


public class Leaderboard : MonoBehaviour
{
    [SerializeField] ObjectsUI objectsUI;
    [SerializeField] LeaderboardMode mode = LeaderboardMode.GetHighScoresAndPlayerScore;
    [FormerlySerializedAs("nr")] [SerializeField] int maxNrToShow;

    public void FetchTopHighScores(string leaderboardID){
        LootLockerSDKManager.GetScoreList(leaderboardID, 9999, OnGetScores);
    }

    void OnGetScores(LootLockerGetScoreListResponse response)
    {
        if (!response.success){
            Debug.LogWarning($"Failed to get scores. Status code: {response.statusCode}", this);
            return;
        }
        var scores = response.items;
        var scoresNr = Mathf.Min(scores.Length, maxNrToShow);
        objectsUI.UpdateUI(scoresNr);
        var playerID = PlayerName.GetPlayerID();
        var startIndex = 0;
        var endIndex = scoresNr;
        if (mode != LeaderboardMode.GetHighScores){
            var playerRank = Array.FindIndex(scores, score => score.member_id == playerID);
            if (mode == LeaderboardMode.GetHighScoresAndPlayerScore){
                if (playerRank >= endIndex){
                    endIndex -= 1;
                    ShowScore(playerRank, endIndex);
                }
            }
            if (mode == LeaderboardMode.GetPlayerScore){
                startIndex = Mathf.Min(0, Mathf.FloorToInt(playerRank - (maxNrToShow - 1) / 2f));
                endIndex = Mathf.Min(startIndex + scoresNr);
            }
        }
        var index = 0;
        for (var i = startIndex; i < endIndex; i++){
            ShowScore(i, index);
            index++;
        }

        void ShowScore(int scoreIndex, int objectUIIndex)
        {
            var obj = objectsUI.GetActiveObjs()[objectUIIndex];
            var statPanel = obj.GetComponent<StatPanel>();
            var score = scores[scoreIndex];
            var playerName = score.player.name;
            var isThisPlayer = score.member_id == playerID;
            if (playerName == ""){
                playerName = isThisPlayer ? "You" : "Guest";
            }
            var textToShow = $"{scoreIndex}. {playerName}";
            if (isThisPlayer)
            {
                textToShow = $"{scoreIndex}. <color=red>{playerName}</color>";
            }
            statPanel.description.UpdateUI(textToShow);
            statPanel.value.UpdateUI(score.score);
        }
    }

    // IEnumerator FetchTopHighScoresRoutine(string leaderboardID){
    //     bool done = false;
    //     
    //     // if (getSurroundingScores){
    //     //     yield return GetSurroundingScores();
    //     //     yield break;
    //     // }
    //     // LootLockerSDKManager.GetScoreList(LeaderboardID, maxNrToShow, 0, (response) => {
    //     //     if (response.success){
    //     //         string playerNames = "";
    //     //         string playerScores = "";
    //     //         LootLockerLeaderboardMember[] members = response.items;
    //     //         foreach (LootLockerLeaderboardMember member in members){
    //     //             playerNames += member.rank + ". ";
    //     //             if (member.player.id.ToString() == PlayerPrefs.GetString("PlayerID", "")){
    //     //                 if (member.player.name != "")
    //     //                     playerNames += "<color=red>" + member.player.name + "</color>";
    //     //                 else
    //     //                     playerNames += "<color=red>You</color>";
    //     //             }
    //     //             else if (member.player.name != ""){
    //     //                 playerNames += member.player.name;
    //     //             } else {
    //     //                 playerNames += "Guest";
    //     //             }
    //     //             string score = member.score.ToString();
    //     //             playerScores += score + "\n";
    //     //             playerNames += "\n";
    //     //         }
    //     //         playerNamesText.text = playerNames;
    //     //         playerScoresText.text = playerScores;
    //     //     } else {
    //     //         Debug.Log("Failed to get top scores");
    //     //     }
    //     //     done = true;
    //     // });
    //     yield return new WaitWhile(() => !done);
    // }
    //
    // IEnumerator GetSurroundingScores(){
    //     yield return new WaitForSeconds(1);
    //     bool done = false;
    //     LootLockerSDKManager.GetMemberRank(LeaderboardID, PlayerPrefs.GetString("PlayerID", ""), (response) =>
    //     {
    //         if (response.statusCode == 200)
    //         {
    //             int rank = response.rank;
    //             int count = maxNrToShow;
    //             int after = rank < maxNrToShow ? 0 : rank - Mathf.CeilToInt(maxNrToShow/2f);
    //
    //             LootLockerSDKManager.GetScoreList(LeaderboardID, count, after, (response) =>
    //             {
    //                 if (response.statusCode == 200)
    //                 {
    //                     Debug.Log("Successful");
    //                     string playerNames = "";
    //                     string playerScores = "";
    //                     LootLockerLeaderboardMember[] members = response.items;
    //                     foreach (LootLockerLeaderboardMember member in members)
    //                     {
    //                         playerNames += member.rank + ". ";
    //                         if (member.player.id.ToString() == PlayerPrefs.GetString("PlayerID", "")){
    //                             if (member.player.name != "")
    //                                 playerNames += "<color=red>" + member.player.name + "</color>";
    //                             else
    //                                 playerNames += "<color=red>You</color>";
    //                         }
    //                         else if (member.player.name != "")
    //                         {
    //                             playerNames += member.player.name;
    //                         }
    //                         else
    //                         {
    //                             playerNames += "Guest";
    //                         }
    //                         string score = member.score.ToString();
    //                         playerScores += score + "\n";
    //                         playerNames += "\n";
    //                     }
    //                     playerNamesText.text = playerNames;
    //                     playerScoresText.text = playerScores;
    //                 }
    //                 else
    //                 {
    //                     Debug.Log("failed: " + response.Error);
    //                 }
    //             });
    //         }
    //         else
    //         {
    //             Debug.Log("failed: " + response.Error);
    //         }
    //         done = true;
    //     });
    //     yield return new WaitWhile(() => !done);
    // }
}

enum LeaderboardMode
{
    GetHighScoresAndPlayerScore,
    GetPlayerScore,
    GetHighScores
}
