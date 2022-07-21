using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionScreen : MonoBehaviour
{
    public Transform optionsScreenObject;
    public Transform[] selectionScreenButtons;
    public Transform menuManagerObject;

    MenuManager menuManager;

    Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new Button[selectionScreenButtons.Length];

        menuManager = menuManagerObject.GetComponent<MenuManager>();

        for (int i = 0; i < selectionScreenButtons.Length; i++)
        {
            buttons[i] = selectionScreenButtons[i].GetComponent<Button>();
        }

        buttons[0].onClick.AddListener(menuManager.changeScene);

        buttons[1].onClick.AddListener(menuManager.changeScene);

        buttons[2].onClick.AddListener(menuManager.changeToOptions);

        buttons[3].onClick.AddListener(menuManager.ExitGame);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
