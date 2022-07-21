using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float velocity = 12;

    Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;

    [Header("Damage Info")]
    public float amount;
    public DamageInfo.DamageType type;

    DamageInfo damageInfo;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemigos"))
        {
            collision.gameObject.GetComponent<IDamage>().TakeDamage(damageInfo);
            Death();
        }
    }
}