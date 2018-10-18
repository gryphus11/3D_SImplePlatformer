using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public int currentLevel = 1;
    public int highestLevel = 2;

    [SerializeField]
    private CHudManager _hudManager = null;

    public static CGameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _hudManager = FindObjectOfType<CHudManager>();
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("Game Manager Start");

    }

    public void AddScore(int amount)
    {
        score += amount;

        if (_hudManager != null)
        {
            Debug.Log("Coin!!!!!!!!!!!");
            _hudManager.ResetHud();
        }

        if (highScore < score)
        {
            highScore = score;
            Debug.Log("new High Score : " + highScore);
        }

        Debug.Log("new Score : " + score);
    }

    public void IncreaseLevel()
    {
        if (currentLevel < highestLevel)
        {
            ++currentLevel;
        }
        else
        {
            currentLevel = 1;
        }

        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void ResetGame()
    {
        score = 0;
        currentLevel = 1;
        if (_hudManager != null)
        {
            _hudManager.ResetHud();
        }
        SceneManager.LoadScene("Level1");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void OnLevelWasLoaded(int level)
    {
        _hudManager = FindObjectOfType<CHudManager>();
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy GameManager");
    }
}
