using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerData : MonoBehaviour {
    public static int lives = 3;
    public static int score = 0;
    public static int scoreRecord = 0;
    public static int blockCount = 0;

    private GameObject gameOverObj;
    private bool gameOver = false;

    public string nextLevel = "NextLevelName";

    void Awake()
    {
        lives = PlayerPrefs.GetInt("lives");
        lives = lives > 0 ? lives : 3;
        score = PlayerPrefs.GetInt("score");
        scoreRecord = PlayerPrefs.GetInt("scoreRecord");

        blockCount = GameObject.FindGameObjectsWithTag("Block").Length;
        gameOverObj = GameObject.FindGameObjectWithTag("GameOver");
        gameOverObj.SetActive(false);
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
        if (!gameOver)
        {
            Debug.Log("¡¡¡¡GAME OVER!!!!");
            // Display text
            Debug.Log("gameOverObj.active: " + gameOverObj.activeSelf);
            if (gameOverObj != null)
            {
                gameOverObj.SetActive(true);
            }

            // Check Record
            if (score > scoreRecord)
            {
                scoreRecord = score;
            }

            // Reset variables
            PlayerPrefs.SetInt("lives", 3);
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("scoreRecord", scoreRecord);

            gameOver = true;
        }
        else
        {
            var shoot = Input.GetButton("Jump");

            if (shoot)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
