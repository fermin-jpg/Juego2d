using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float Duration;
    public int value;

    public AudioSource pickup;

    float maxDuration;
    bool upAlpha;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        maxDuration = Duration;
        upAlpha = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Duration > 0)
        {
            Duration -= Time.deltaTime;
            if(Duration < (maxDuration/4.0f))
            {
                Tingle();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pickup.Play();
            spriteRenderer.enabled = false;
            collision.gameObject.GetComponent<Player>().PickUpMana(value);
            Invoke("Destroy", 1);
        }
    }
    void Tingle()
    {
        float maxAlpha = 1.0f;
        float minAlpha = 0.0f;
        float timeMultiplicator = 5.0f;

        Color auxColor = new Color(0,0,0,Time.deltaTime*timeMultiplicator);

        if(!upAlpha)
        {
            spriteRenderer.color -= auxColor;
            if(spriteRenderer.color.a <= minAlpha)
            {
                upAlpha = true;
            }
        }
        else
        {
            spriteRenderer.color += auxColor;
            if (spriteRenderer.color.a >= maxAlpha)
                upAlpha = false;
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }




}
