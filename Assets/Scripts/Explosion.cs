using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public LayerMask layerMask;

    DamageInfo damageInfo;
    void Start()
    {
        damageInfo = new DamageInfo(40, DamageInfo.DamageType.fire);

        Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, 2, layerMask);
        if (collider2D.Length > 0)
        {
            for (int i = 0; i < collider2D.Length; i++)
            {
                collider2D[i].GetComponent<IDamage>().TakeDamage(damageInfo);
            }
        }

        Invoke("Death", 1);
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
