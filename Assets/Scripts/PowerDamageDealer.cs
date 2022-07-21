using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDamageDealer : MonoBehaviour
{

    [Header("Damage Info")]
    public float amount;
    public DamageInfo.DamageType type;

    [Header("Effect Info")]
    public float duration;
    public float power;
    public EffectInfo.EffectType effect;

    DamageInfo damageInfo;
    EffectInfo effectInfo;

    // Start is called before the first frame update
    void Start()
    {
        damageInfo = new DamageInfo(amount, type);
        if (duration != 0)
            effectInfo = new EffectInfo(duration, power, effect);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemigos"))
        {
            collision.gameObject.GetComponent<IDamage>().TakeDamage(damageInfo);
            if (effectInfo != null)
                collision.gameObject.GetComponent<IEffects>().GetStatusEffect(effectInfo);
        }
    }
}

