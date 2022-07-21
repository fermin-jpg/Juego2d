using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject MenuPausa;
    public AudioSource backgroundMusic;
    public AudioSource[] EffectsAudio;
    public Slider[] soundSliders;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            soundSliders[i].value = 0.5f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        backgroundMusic.volume = soundSliders[0].value;

        for (int i = 0; i < EffectsAudio.Length; i++)
        {
            EffectsAudio[i].volume = soundSliders[1].value;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            MenuPausa.SetActive(true);
            Debug.Log("gdsgfd");
           
        }
    }
    public void backMenu()
    {
        MenuPausa.SetActive(false);
        Time.timeScale = 1f;
    }
}
