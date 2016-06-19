using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerData : MonoBehaviour {
    public static int lives = 3;
    public static int score = 0;
    public static int scoreRecord = 0;
    public static int blockCount = 0;

    public string nextLevel = "NextLevelName";

    void Awake()
    {
        lives = PlayerPrefs.GetInt("lives");
        lives = lives > 0 ? lives : 3;
        score = PlayerPrefs.GetInt("score");
        scoreRecord = PlayerPrefs.GetInt("scoreRecord");

        blockCount = GameObject.FindGameObjectsWithTag("Block").Length;
    }

    void Update()
    {
        if (lives <= 0)
        {
            GameOver();
        }
        else if (blockCount <= 0)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        PlayerPrefs.SetInt("lives", lives);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("scoreRecord", scoreRecord);

        // Application.LoadLevel(nextLevel);
        SceneManager.LoadScene(nextLevel);
    }

    private void GameOver()
    {
        // Check Record
        if (score > scoreRecord)
        {
            scoreRecord = score;
        }

        // Reset variables
        PlayerPrefs.SetInt("lives", 3);
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("scoreRecord", scoreRecord);
    }
}
