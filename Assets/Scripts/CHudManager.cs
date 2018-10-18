using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHudManager : MonoBehaviour
{
    public Text scoreLabel = null;

    // Use this for initialization
    void Start()
    {
        ResetHud();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetHud()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score : " + CGameManager.instance.score;
        }
    }
}
