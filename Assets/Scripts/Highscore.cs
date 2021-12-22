using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public GameObject highscore;
    public GameObject mainMenu;


    public TextMeshProUGUI[] HighscoreText = new TextMeshProUGUI[5];

    private void Start()
    {
        for (int i = 0; i < HighscoreText.Length; i++)
        {
            HighscoreText[i].text = PlayerPrefs.GetInt("Highscore" + i).ToString();
        }
    }
    

 
}
