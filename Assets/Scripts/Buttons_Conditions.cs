using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_Conditions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scene()
    {
        SceneManager.LoadScene("3-Level 1-1");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
