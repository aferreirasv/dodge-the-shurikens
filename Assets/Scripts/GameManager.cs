using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    bool gameOver;
    public AudioMixer audioMixer;
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI levelTMP;
    public TextMeshProUGUI totalScoreTMP;
    public TextMeshProUGUI scoreLabelTMP;
    public GameObject PauseScreen;
    public GameObject GameOverScreen;
    public GameState gameState = new GameState();
    public float shurikenMaxSpeed;
    public GameObject sushiPrefab;
    public GameObject shurikenPrefab;
    public GameObject goldenPrefab;
    public GameObject shieldPrefab;
    public int level;
    public int[] levelMilestones = new int[] {100, 1000, 2000, 3000, 4000, 5000, 7500, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 60000, 70000, 80000, 90000, 100000 };
    bool paused = false;
    Shuriken lastShuriken;


    private void Start()
    {
        Time.timeScale = 1;
        shurikenMaxSpeed = 30f;
        gameOver = false;
        level = 0;
        Instantiate(sushiPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (gameOver)
            {
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyDown("space") && gameOver)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void PauseGame()
    {
        if (paused)
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
            paused = false;
            audioMixer.SetFloat("volume", 0);
        }
        else
        {
            Time.timeScale = 0;
            audioMixer.SetFloat("volume", -80);
            PauseScreen.SetActive(true);
            paused = true;
        }

    } 

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void UpdateScore(int score)
    {
        gameState.AddScore(score);
        scoreTMP.text = "Score: "+ gameState.GetScore().ToString();
        if(gameState.GetScore() >= levelMilestones[level])
        {
            level ++;
            levelTMP.text = "Level: " + level.ToString();
            if ((level % 3) == 0)
            {
                Instantiate(sushiPrefab);
            }
            else if(level >= 4 && level % 2 == 0)
            {
                if (UnityEngine.Random.Range(0, 21) == 0) Instantiate(shieldPrefab);
                Instantiate(goldenPrefab);
            }
            else
            {
                GameObject shurikenObject = Instantiate(shurikenPrefab);
                Shuriken shuriken = shurikenObject.GetComponent<Shuriken>();
                if(lastShuriken != null)
                {
                    shuriken.Create(lastShuriken.rb.position);
                }
                else
                {
                    shuriken.Create(GetRandomPosition());
                }
                lastShuriken = shuriken;
            }
            
        }
    }

    public void GameOver()
    {
        bool highscoreSetted = InsertHighscore(gameState.GetScore());
        if (highscoreSetted)
        {
            scoreLabelTMP.text = "New Highscore!";
        }
        else
        {
            scoreLabelTMP.text = "Total Score";
        }
        totalScoreTMP.text = gameState.GetScore().ToString();
        GameOverScreen.SetActive(true);
        gameOver = true;
    }

    public bool InsertHighscore(int score)
    {
        int highscore = 0;
        int previousHighscore;
        bool highscoreSetted = false;

        for (int i = 0; i < 5; i++)
        {
            previousHighscore = highscore;
            highscore = PlayerPrefs.GetInt("Highscore" + i);
            if (highscoreSetted)
            {
                PlayerPrefs.SetInt("Highscore" + i, previousHighscore);
                continue;
            }
            if (score > highscore)
            {
                PlayerPrefs.SetInt("Highscore" + i, score);
                highscoreSetted = true;
            }

        }
        return highscoreSetted;
    }

    
    public int GetDirection()
    {
        int result = 0;
        while(result == 0)
        {
            result = UnityEngine.Random.Range(-1, 2);
        }
        return result;
    }

    public Vector2 GetRandomPosition()
    {
        return new Vector2(UnityEngine.Random.Range(gameState.min.x, gameState.max.x), UnityEngine.Random.Range(gameState.min.y, gameState.max.y));
    }

}

public class GameState
{
    int score = 0;
    public Vector2 max = new Vector2(6.5f, 3.5f);
    public Vector2 min = new Vector2(-6.5f, -3.5f);

    public void AddScore(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return this.score;
    }

}
