using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;

    public float duration;

    public Vector3 currentDirection;

    DamageInfo damageInfo;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Fire(Vector3 direction, bool facingRight)
    {
        Debug.Log(" " + facingRight);
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = facingRight;
        currentDirection = direction.normalized;
    }
    // Update is called once per frame
    void Update()
    {
        if (damageInfo == null)
            damageInfo = new DamageInfo(damage, DamageInfo.DamageType.physical);
        transform.position += currentDirection * speed * Time.deltaTime;
        if (duration > 0)
            duration -= Time.deltaTime;
        else
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<IDamage>().TakeDamage(damageInfo);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            Destroy(this.gameObject);
    }
}

