using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public float duration;
    public float fallingSpeed;

    public float fireDamage = 10.0f;
    public float fireDuration = 4.0f;

    Animator anim;
    Rigidbody2D rigidbody;

    EffectInfo burningEffect;

    // Start is called before the first frame update
    void Start()
    {
        fallingSpeed = 5.0f;
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        burningEffect = new EffectInfo(fireDuration, fireDamage, EffectInfo.EffectType.burn);
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("onAir")==true)
        {
            transform.position -= Vector3.up * fallingSpeed * Time.deltaTime;
        }
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            anim.SetBool("onAir", false);
            Destroy(rigidbody);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<IEffects>().GetStatusEffect(burningEffect);
        }
    }
    //poison, slow, burned, freeze
}
