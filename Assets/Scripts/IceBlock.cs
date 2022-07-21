using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
    private void Start()
    {
        Invoke("Death", 7);
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
