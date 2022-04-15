using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public float scoreMult = 2;
    public TextMeshProUGUI scoreText;

    public ScoreCounter scoreCounter;

    // Update is called once per frame
    void Update()
    {
        scoreCounter.score += scoreMult * Time.deltaTime;

        scoreText.text = "Score: " + Mathf.Floor(scoreCounter.score) + ". Highscore: " + Mathf.Floor(scoreCounter.scoreHighscore);
    }
}
