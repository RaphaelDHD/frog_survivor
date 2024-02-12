using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image imageBackground;
    public Image imageTitle;
    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        // set the background image to the screen size
        imageBackground.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        // set the image title to a ratio of 1200 / 300 based on the screen size
        imageTitle.rectTransform.sizeDelta = new Vector2(Screen.width * 0.85f, Screen.height * 0.1f);
        playButton.onClick.AddListener(PlayGame);
    }

    void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
