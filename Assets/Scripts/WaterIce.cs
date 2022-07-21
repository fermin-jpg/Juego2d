using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIce : MonoBehaviour
{
    public float velocity = 12;

    Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;

    [Header("Damage Info")]
    public float amount;
    public DamageInfo.DamageType type;


    [Header("Effect Info")]
    public float duration;
    public float power;
    public EffectInfo.EffectType effect;


    DamageInfo damageInfo;
    EffectInfo effectInfo;

    public void Death()
    {
        Destroy(gameObject);
    }

    public void InitializePush(bool facingLeft)
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        damageInfo = new DamageInfo(amount, type);
        effectInfo = new EffectInfo(duration, power, effect);

        _animator.SetTrigger("activated");

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        _rigidbody.velocity = new Vector2(0, 0);
        _rigidbody.angularVelocity = 0;
        _spriteRenderer.flipX = facingLeft;


        if (facingLeft)
            _rigidbody.AddForce(new Vector3(-1, 0, 0) * velocity, ForceMode2D.Impulse);

        else
            _rigidbody.AddForce(new Vector3(1, 0, 0) * velocity, ForceMode2D.Impulse);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemigos"))
        {
            collision.gameObject.GetComponent<IDamage>().TakeDamage(damageInfo);
            collision.gameObject.GetComponent<IEffects>().GetStatusEffect(effectInfo);
            Death();
        }
            
    }
}