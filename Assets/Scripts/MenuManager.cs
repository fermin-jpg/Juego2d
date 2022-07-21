using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform selectionScreenObject;
    public Transform optionsScreenObject;
    public Transform showPoint;
    public Transform hidePoint;

    enum State 
    { 
        continueGame,
        selectionToContinue,
        selection,
        selectionToOptions,
        options,
        optionsToSelection
    }

    State state;
    State nextState;

    public float transitionTime;

    float transitionTimer;

    bool hidding;

    void Start()
    {
        state = State.selection;
        nextState = State.selection;
        
        optionsScreenObject.gameObject.SetActive(false);
        selectionScreenObject.gameObject.SetActive(true);

        transitionTimer = transitionTime;
        hidding = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (nextState != state) 
        {
            if (nextState == State.continueGame)
            {
                Debug.Log("Estoy cambiando escena");

                SceneManager.LoadScene("LevelSelectionMenu", LoadSceneMode.Single);
            }
            if (nextState == State.selection)
            {
                Debug.Log("Estoy en selection");
            }

            if (nextState == State.options)
            {
                Debug.Log("Estoy en options");
            }

            state = nextState;

        }

        if (state == State.selection)
        {
            
        }

        if (state == State.selectionToOptions)
        {

            if (hidding == true)
            {
                transitionTimer -= Time.deltaTime;

                if (transitionTimer > 0)
                {
                    selectionScreenObject.position = hidePoint.position + (showPoint.position - hidePoint.position) * (transitionTimer / transitionTime);
                }
                else 
                {
                    optionsScreenObject.position = hidePoint.position;
                    selectionScreenObject.gameObject.SetActive(false);
                    optionsScreenObject.gameObject.SetActive(true);
                    hidding = false;
                    transitionTimer = transitionTime;
                }
            }
            else 
            {
                transitionTimer -= Time.deltaTime;

                if (transitionTimer > 0)
                {
                    optionsScreenObject.position = showPoint.position + (hidePoint.position - showPoint.position) * (transitionTimer / transitionTime);
                }
                else
                {
                    hidding = true;
                    transitionTimer = transitionTime;
                    nextState = State.options;
                }
            }
        }

        if (state == State.optionsToSelection)
        {

            if (hidding == true)
            {
                transitionTimer -= Time.deltaTime;

                if (transitionTimer > 0)
                {
                    optionsScreenObject.position = hidePoint.position + (showPoint.position - hidePoint.position) * (transitionTimer / transitionTime);
                }
                else
                {
                    selectionScreenObject.position = hidePoint.position;
                    optionsScreenObject.gameObject.SetActive(false);
                    selectionScreenObject.gameObject.SetActive(true);
                    hidding = false;
                    transitionTimer = transitionTime;
                }
            }
            else
            {
                transitionTimer -= Time.deltaTime;

                if (transitionTimer > 0)
                {
                    selectionScreenObject.position = showPoint.position + (hidePoint.position - showPoint.position) * (transitionTimer / transitionTime);
                }
                else
                {
                    hidding = true;
                    transitionTimer = transitionTime;
                    nextState = State.selection;
                }
            }
        }

        if (state == State.selectionToContinue) 
        {
            nextState = State.continueGame;
        }

        if (state == State.options)
        {

        }

    }

    public void changeToSelection()
    {
        nextState = State.optionsToSelection;
    }

    public void changeToOptions()
    {
        nextState = State.selectionToOptions;
    }

    public void changeScene()
    {
        nextState = State.selectionToContinue;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
