using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CGameOverManager : MonoBehaviour
{
    public Text scoreValue = null;
    public Text highScoreValue = null;

    // Use this for initialization
    void Start()
    {
        if (scoreValue != null)
        {
            scoreValue.text = CGameManager.instance.score.ToString();
        }

        if (highScoreValue != null)
        {
            highScoreValue.text = CGameManager.instance.highScore.ToString();
        }

        PlayerPrefs.SetInt("HighScore", CGameManager.instance.highScore);
    }

    public void ResetGame()
    {
        CGameManager.instance.ResetGame();
        SceneManager.LoadScene("Level1");
    }
}
