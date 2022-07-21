using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [Header("Damage Info")]
    public float amount;
    public DamageInfo.DamageType type;

    Rigidbody2D _rigidbody;

    DamageInfo damageInfo;

    void Start()
    {
        damageInfo = new DamageInfo(amount, type);
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
            collision.gameObject.GetComponent<IDamage>().TakeDamage(damageInfo);
        Destroy(gameObject);
    }
}