using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusCanvas : MonoBehaviour
{
    [Header("Effect Sprites")]
    public Sprite burnSprite;
    public Sprite poisonSprite;
    public Sprite slowSprite;
    public Sprite freezeSprite;

    public Enemies enemy;
    public Player Gaia;

    Image EffectDamage;
    Image EffectMovement;

    // Start is called before the first frame update
    void Start()
    {
        EffectDamage = transform.Find("Status Effects").transform.Find("EffectL").GetComponent<Image>();
        EffectMovement = transform.Find("Status Effects").transform.Find("EffectR").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        SetStatusImage();
    }

    public void SetStatusImage()
    {
        if(Gaia == null)
        {
            if (enemy.poisoned)
            {
                EffectDamage.enabled = true;
                EffectDamage.sprite = poisonSprite;
            }
            else if (enemy.burned)
            {
                EffectDamage.enabled = true;
                EffectDamage.sprite = burnSprite;
            }
            else
            {
                EffectDamage.enabled = false;
            }

            if (enemy.slowed)
            {
                EffectMovement.enabled = true;
                EffectMovement.sprite = slowSprite;
            }
            else if (enemy.frozen)
            {
                EffectMovement.enabled = true;
                EffectMovement.sprite = freezeSprite;
            }
            else
            {
                EffectMovement.enabled = false;
            }
        }
        else
        {
            if (Gaia.poisoned)
            {
                EffectDamage.enabled = true;
                EffectDamage.sprite = poisonSprite;
            }
            else if (Gaia.burned)
            {
                EffectDamage.enabled = true;
                EffectDamage.sprite = burnSprite;
            }
            else
            {
                EffectDamage.enabled = false;
            }

            if (Gaia.slowed)
            {
                EffectMovement.enabled = true;
                EffectMovement.sprite = slowSprite;
            }
            else if (Gaia.frozen)
            {
                EffectMovement.enabled = true;
                EffectMovement.sprite = freezeSprite;
            }
            else
            {
                EffectMovement.enabled = false;
            }
        }
        
    }



}
