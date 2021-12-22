using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject highscore;
    public GameObject settings;
    public AudioMixer audioMixer;
    int shurikenFrame = 0;

    public TextMeshProUGUI[] HighscoreText = new TextMeshProUGUI[5];
    public Image[] imageComponent;

    private void Start()
    {
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
        Debug.Log(PlayerPrefs.GetFloat("volume"));
        Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen") > 0;
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        if (PlayerPrefs.GetInt("ResolutionX") != 0)
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionX"), PlayerPrefs.GetInt("ResolutionY"), Screen.fullScreen);
        }
    }

    private void FixedUpdate()
    {
        foreach (Image image in imageComponent)
        {
            image.sprite = sprites[shurikenFrame];
        }
        shurikenFrame++;
        if (shurikenFrame > 3) shurikenFrame = 0;

        if (Input.GetKeyDown("escape") && highscore.activeSelf)
        {
            Highscores();
        }

    }

    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Highscores()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        highscore.SetActive(!highscore.activeSelf);
        if (highscore.activeSelf)
        { 
            for (int i = 0; i < HighscoreText.Length; i++)
            {
                HighscoreText[i].text = PlayerPrefs.GetInt("Highscore" + i).ToString();
            }
        }
    }

    public void Settings()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        settings.SetActive(!settings.activeSelf);
    }

    public void Options()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public Sprite[] sprites;
}
