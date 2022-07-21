using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoticarioJungla_AnimationEvents : MonoBehaviour
{
    private BoticarioJungla boticario;

    // Start is called before the first frame update
    void Start()
    {
        boticario = transform.parent.GetComponent<BoticarioJungla>();
    }

    void Fire()
    {
        boticario.Attack();
    }

    void finishHit()
    {
        boticario.recentlyHit = false;
    }
}
