using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndGameTrigger : MonoBehaviour
{
    public Transform canvasPause;
    public bool win = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0f;
            win = true;

            ActivateCanvas(true);
        }
    }

    public void ActivateCanvas(bool _winOrLose)
    {
        Time.timeScale = 0f;
        canvasPause.gameObject.SetActive(true);
        if (_winOrLose)
        {
            canvasPause.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You Won";
        }
        else
            canvasPause.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You Died";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
