using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerritoryManager : MonoBehaviour
{
    MenuManager menuManager;
    GameManager gameManager;

    public enum State
    {
        none,
        Jungle,
        Ice,
        Water,
        Desert,
        City
    }

    State state;
    State NexState;

    // Start is called before the first frame update
    void Start()
    {
        //Que empieze con ningun terreno seleccionado

        state = State.none;
        NexState = State.none;
    }

    // Update is called once per frame
    void Update()
    {
        if (state != NexState) //este if dejarlo siempre al final del update
        {
            state = NexState;
        }
    }

    public void SelectorTerritorio(State Territory) //Funcion que pasamos por párametro el state del territorio
    {
        if (Territory == State.Jungle) //Si has seleccionado el terriorio Jungla,que pase algo
        {
            NexState = State.Jungle;
            Debug.Log("Territorio Seleccionado : JUNGLA");
        }

        if (Territory == State.Ice) //Si has seleccionado el terriorio de Hielo,que pase algo
        {
            NexState = State.Ice;
            Debug.Log("Territorio Seleccionado : HIELO");
        }

        if (Territory == State.Water) //Si has seleccionado el terriorio de Agua,que pase algo
        {
            NexState = State.Water;
            Debug.Log("Territorio Seleccionado : AGUA");
        }

        if (Territory == State.Desert) //Si has seleccionado el terriorio Desierto,que pase algo
        {
            NexState = State.Desert;
            Debug.Log("Territorio Seleccionado : DESIERTO");
        }

        if (Territory == State.City) //Si has seleccionado el terriorio de Ciudad,que pase algo
        {
            NexState = State.City;
            Debug.Log("Territorio Seleccionado : CIUDAD");
        }
    }
}
