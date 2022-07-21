using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    public Transform[] optionsScreenButtons;
    public Transform menuManagerObject;
    public Transform backgroundMusicObject;

    bool isFullScreen;

    MenuManager menuManager;
    Button backButton;
    Slider[] soundSliders;
    AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = menuManagerObject.GetComponent<MenuManager>();

        backButton = optionsScreenButtons[0].GetComponent<Button>();

        backgroundMusic = backgroundMusicObject.GetComponent<AudioSource>();

        soundSliders = new Slider[2];

        for (int i = 0; i < 2; i++)
        {
            soundSliders[i] = optionsScreenButtons[i + 1].GetComponent<Slider>();

            soundSliders[i].value = 1.0f;
        }

        backButton.onClick.AddListener(menuManager.changeToSelection);

        isFullScreen = true;

    }

    // Update is called once per frame
    void Update()
    {
        backgroundMusic.volume = soundSliders[0].value;
    }

    public void FullScreen(bool value)
    {
        Screen.fullScreen = value;
        isFullScreen = value;
    }

    public void ResolutionChange(int value)
    {
        if (value == 0)
        {
            Debug.Log("Resolucion actual: 1920x1080");
            Screen.SetResolution(1920, 1080, isFullScreen);
        }
        if (value == 1)
        {
            Debug.Log("Resolucion actual: 1366×768");
            Screen.SetResolution(1366, 768, isFullScreen);
        }
        if (value == 2)
        {
            Debug.Log("Resolucion actual: 1280x720");
            Screen.SetResolution(1280, 720, isFullScreen);
        }
    }

}
