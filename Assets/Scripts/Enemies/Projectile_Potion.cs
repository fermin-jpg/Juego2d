using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Potion : MonoBehaviour
{
    public float speed;

    public GameObject firePrefab;

    public float duration;

    Rigidbody2D rigidBody;
    Transform player;

    EffectInfo slowPotionEffect;

    Vector3 targetDirection;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (firePrefab == null)
        {
            slowPotionEffect = new EffectInfo(3.0f, 50.0f, EffectInfo.EffectType.slow);
        }
        targetDirection = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        ForwardAdvance();
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
            Destroy(this.gameObject);
    }

    void Rotate()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime, Space.Self);
    }

    void ForwardAdvance()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (firePrefab != null)
                Instantiate(firePrefab, transform.position, Quaternion.identity);
            else if (firePrefab == null && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                collision.gameObject.GetComponent<IEffects>().GetStatusEffect(slowPotionEffect);
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        if (firePrefab != null)
    //            Instantiate(firePrefab, transform.position, Quaternion.identity);
    //        else if (firePrefab == null && other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //            other.gameObject.GetComponent<IEffects>().GetStatusEffect(slowPotionEffect);
    //        Destroy(this.gameObject);
    //    }
    //}
}
