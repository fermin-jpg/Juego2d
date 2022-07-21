using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager2 : MonoBehaviour
{
    TerritoryManager territoryManager;

    public enum State
    {
        Menu,
        Play,
        Territory,
        GameOver
    }

    State state;
    State NexState;

    void Start()
    {
       
        //Empezar en el estado de Menú,que seria donde eligies por ejemplo: 
        //Jugar,Opciones,salir del juego,etc...

        //En state Menu mostrar el menu
        state = State.Menu;
        NexState = State.Menu;

    }


    void Update()
    {
        //DE Ejemplo: al pulsar click izquierdo cambiar de escena

        //Si le das Clic al boton De jugar,que te cambie de escena al selector de territorios

            //if (Input.GetMouseButtonDown(0)) 
            //{
            //    SceneManager.LoadScene("2-SelectorTerritoryScene");
            //}


        //Si le das Click al terriorio de Jungla,entonces llamamos a la funcion "SelectorTerritorio" de TerritoryManager
        //y le pasamos por parametro el State a Jungle.

        // territoryManager.SelectorTerritorio(TerritoryManager.State.Jungle);



        if (state != NexState) //este if dejarlo siempre al final del update
        {
            state = NexState;
        }
    }
}
