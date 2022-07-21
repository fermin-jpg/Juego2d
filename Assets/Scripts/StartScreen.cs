using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public Transform menuManagerObject;

    MenuManager menuManager;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = menuManagerObject.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //elements[0];

        if (Input.GetMouseButtonDown(0))
        {
            menuManager.changeToSelection();
        }

    }
}
