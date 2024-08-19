using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowLeaderboard : MonoBehaviour
{
    public Leaderboard leaderboard;

    public void Show(int nr){
        leaderboard.FetchTopHighScores("Level" + (nr + 1));
    }

    public void Show()
    {
        leaderboard.FetchTopHighScores(SceneManager.GetActiveScene().name.Replace(" ", ""));
    }
}
