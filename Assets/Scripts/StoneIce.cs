﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneIce : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    DamageInfo damageInfo;
    EffectInfo effectInfo;

    [Header("Damage Info")]
    public float amount;
    public DamageInfo.DamageType type;


    [Header("Effect Info")]
    public float duration;
    public float power;
    public EffectInfo.EffectType effect;

    void Start()
    {
        damageInfo = new DamageInfo(amount, type);
        effectInfo = new EffectInfo(duration, power, effect);
    }
    public void InitializePush(float angle)
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        _rigidbody.velocity = new Vector2(0, 0);
        _rigidbody.angularVelocity = 0;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);

        _rigidbody.AddForce(new Vector2(x, y) * 9, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemigos"))
        {
            collision.gameObject.GetComponent<IDamage>().TakeDamage(damageInfo);
            collision.gameObject.GetComponent<IEffects>().GetStatusEffect(effectInfo);
        }
        Destroy(gameObject);
    }
}