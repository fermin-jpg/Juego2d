using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonSelected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChancheEscene()
    {
        SceneManager.LoadScene("3-Level_1-1");
    }
    public void BotonAtras()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EntrarSeleccionNive()
    {
        SceneManager.LoadScene("LevelSelectionMenu");
    }
}
