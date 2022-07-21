using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter_AnimationEvents : MonoBehaviour
{
    private Cazador cazador;

    public AudioSource Shoot;

    // Start is called before the first frame update
    void Start()
    {
        cazador = transform.parent.GetComponent<Cazador>();
    }

    void Fire()
    {
        cazador.RangeAttack();
        Shoot.Play();
    }

    void finishHit()
    {
        cazador.recentlyHit = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}