using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowLeaderboard : MonoBehaviour
{
    public Leaderboard leaderboard;

    public void ShowByLevelNr(int nr){
        leaderboard.FetchTopHighScores("Level" + (nr + 1));
    }

    public void ShowBySceneName()
    {
        leaderboard.FetchTopHighScores(SceneManager.GetActiveScene().name.Replace(" ", ""));
    }
}
