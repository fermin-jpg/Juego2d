using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Parallax : MonoBehaviour
{
    private float length;
    private float StartPosition;
    public float EffectParallax;

    // Start is called before the first frame update
    void Start()
    {
        //coginedo la posición y la anchura 
        StartPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (Camera.main.transform.position.x  * (0.1f - EffectParallax));
        float distance = (Camera.main.transform.position.x * EffectParallax);

        //Si la camara llega al limite del siguiente background,mover el background de la izquierda hacia la derecha
        //Debug.Log("temp" + temp);
        if (temp >= StartPosition + length)
        {
            StartPosition += length;
          //  Debug.Log("parallaxDreta");
        }
        else if(temp < StartPosition-length)  //Si la camara llega al limite del siguiente background,mover el background de la derecha hacia la izquierda
        {
            StartPosition -= length;
            //Debug.Log("parallaxEsquerra");
        }

        transform.position = new Vector3(StartPosition + distance, 7, transform.position.z);
    }
}
