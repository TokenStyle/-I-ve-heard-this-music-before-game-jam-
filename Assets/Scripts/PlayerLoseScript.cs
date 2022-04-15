using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoseScript : MonoBehaviour
{
    public PlayerStats playerStats;
    public ScoreCounter scoreCounter;

    private void Update()
    {
        if (playerStats.Health <= 0)
        {
            // TODO: rewrite it
            playerStats.OnBeforeSerialize();
            scoreCounter.scoreHighscore = scoreCounter.score;
            scoreCounter.score = 0;

            int currentSceneName = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
