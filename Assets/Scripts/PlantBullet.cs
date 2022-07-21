using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    //public GameObject explosionPrefab;
    Vector3 velocity;

    [Header("Damage Info")]
    public float amount;
    public DamageInfo.DamageType type;

    [Header("Effect Info")]
    public float duration;
    public float power;
    public EffectInfo.EffectType effect;

    DamageInfo damageInfo;
    EffectInfo effectInfo;

    bool activated = false;

    void Update()
    {
        if (activated)
        {
            transform.position += velocity;
            if (damageInfo == null)
                damageInfo = new DamageInfo(amount, type);
            if (effectInfo == null)
                effectInfo = new EffectInfo(duration, power, effect);
        }
    }

    public void StartShoot(Vector3 _position)
    {
        velocity = Vector3.Normalize(_position - transform.position) * 2;
        activated = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Instantiate(explosionPrefab);
        if (collision.gameObject.layer == 12)
        {
            collision.gameObject.GetComponent<IDamage>().TakeDamage(damageInfo);
            collision.gameObject.GetComponent<IEffects>().GetStatusEffect(effectInfo);
        }
        Destroy(gameObject);
    }
}
