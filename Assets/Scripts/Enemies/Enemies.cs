using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour, IDamage, IEffects
{
    [SerializeField]
    public float Health { get; set; }
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float patrolSpeed;
    [SerializeField]
    protected float pursueSpeed;
    [SerializeField]
    protected int manaQuantity;

    [SerializeField]
    protected Transform pointR, pointL;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rigidbody;
    protected bool isHit = false;
    public bool isDead = false;
    protected bool facingRight = true;

    protected Transform Player;

    [Header("Resistances")]
    public float physicalRes;
    public float natureRes;
    public float fireRes;
    public float waterRes;

    [Header("Distances")]
    public float distanceToPlayer;
    public float combatDistance;
    public float pursueDistance;
    public float activateDistance;

    [Header("Status Effects")]
    public bool burned;
    public bool poisoned;
    public bool slowed;
    public bool frozen;
    public Material[] statusMaterials;
    public Material standardMaterial;

    [Header("Object to Instantiate at Death")]
    public GameObject ManaPrefab;

    //Status Effects
    protected float burnDuration;
    protected float burnPower;

    protected float poisonDuration;
    protected float poisonPower;

    protected float slowDuration;
    protected float slowPower;

    protected float frozenDuration;

    protected bool CRFire_Started;
    protected bool CRPoison_Started;
    protected bool CRDeath_Started;

    [Header("Common Audio Sources")]
    public AudioSource Death;
    public AudioSource Hit;

    [Header("UI")]
    public HealthBar healthBar;
    public StatusCanvas statusCanvas;

    public bool recentlyHit;
    public bool active;

    public float knockbackForce = 70.0f;


    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        speed = patrolSpeed;

        InitStatusVariables();

        recentlyHit = false;
        distanceToPlayer = Vector3.Distance(transform.position, Player.position);
    }

    public virtual void Movement()
    {
        if (currentTarget.x < transform.position.x)
        {
            facingRight = true;
        }
        if (currentTarget.x >= transform.position.x)
        {
            facingRight = false;
        }
        if (currentTarget == pointR.position && Vector3.Distance(transform.position, currentTarget) < 0.75)
        {
            currentTarget = pointL.position;
            anim.SetTrigger("Idle");
        }
        else if (currentTarget == pointL.position && Vector3.Distance(transform.position, currentTarget) < 0.75)
        {
            currentTarget = pointR.position;
            anim.SetTrigger("Idle");
        }
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        //Debug.Log("Distance to Target: " + Vector3.Distance(transform.position, currentTarget));
        sprite.flipX = facingRight;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        StatusEffects();
    }

    public virtual void TakeDamage(DamageInfo damageinfo)
    {
        if (!isDead)
        {
            float healthLost;
            if (!recentlyHit)
            {
                anim.SetTrigger("Hit");
                Hit.Play();
                recentlyHit = true;
                Knockback();
                switch (damageinfo.type)
                {
                    case (DamageInfo.DamageType.physical):
                        healthLost = damageinfo.amount - (damageinfo.amount * (physicalRes / 100.0f));
                        Health -= (int)healthLost;
                        if (physicalRes > 0)
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.resisted);
                        else if (physicalRes == 0)
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.normal);
                        else //if physicalRes<0
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.vulnerable);
                        break;

                    case (DamageInfo.DamageType.fire):
                        healthLost = damageinfo.amount - (damageinfo.amount * (fireRes / 100));
                        Health -= (int)healthLost;
                        if (fireRes > 0)
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.resisted);
                        else if (fireRes == 0)
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.normal);
                        else //if fireRes<0
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.vulnerable);
                        break;

                    case (DamageInfo.DamageType.nature):
                        healthLost = damageinfo.amount - (damageinfo.amount * (natureRes / 100));
                        Health -= (int)healthLost;
                        if (natureRes > 0)
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.resisted);
                        else if (natureRes == 0)
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.normal);
                        else //if natureRes<0
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.vulnerable);
                        break;

                    case (DamageInfo.DamageType.water):
                        healthLost = damageinfo.amount - (damageinfo.amount * (waterRes / 100));
                        Health -= (int)healthLost;
                        if (waterRes > 0)
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.resisted);
                        else if (waterRes == 0)
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.normal);
                        else //if waterRes<0
                            DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.vulnerable);
                        break;
                }
            }
            if (Health <= 0)
            {
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        if (!CRDeath_Started)
            StartCoroutine("DeathEffect");
    }

    public virtual void Attack()
    {

    }

    public void Knockback()
    {
        if (facingRight)
        {
            rigidbody.AddForce(-transform.right * knockbackForce);
            rigidbody.AddForce(transform.up * knockbackForce);
        }
        else
        {
            rigidbody.AddForce(transform.right * knockbackForce);
            rigidbody.AddForce(transform.up * knockbackForce);
        }
    }

    public void GetStatusEffect(EffectInfo effectinfo)
    {
        switch (effectinfo.type)
        {
            case (EffectInfo.EffectType.burn):
                if (!poisoned)
                {
                    burned = true;
                    burnDuration = effectinfo.duration;
                    burnPower = effectinfo.power;
                }
                break;
            case (EffectInfo.EffectType.freeze):
                frozen = true;
                frozenDuration = effectinfo.duration;
                break;
            case (EffectInfo.EffectType.poison):
                if (!burned)
                {
                    poisoned = true;
                    poisonDuration = effectinfo.duration;
                    poisonPower = effectinfo.power;
                }
                break;
            case (EffectInfo.EffectType.slow):
                if (!frozen)
                {
                    slowed = true;
                    slowDuration = effectinfo.duration;
                    slowPower = effectinfo.power;
                }
                break;
        }
    }

    public void StatusEffects()
    {
        if (burned)
        {
            sprite.material = statusMaterials[(int)EffectInfo.EffectType.burn];
            DamageInfo burnDamage = new DamageInfo(burnPower, DamageInfo.DamageType.fire);
            if (!CRFire_Started)
                StartCoroutine(BurnDamage(burnDamage));
            burnDuration -= Time.deltaTime;
            if (burnDuration <= 0)
            {
                burned = false;
                sprite.material = standardMaterial;
            }

        }
        if (poisoned)
        {
            sprite.material = statusMaterials[(int)EffectInfo.EffectType.poison];
            DamageInfo poisonDamage = new DamageInfo(burnPower, DamageInfo.DamageType.nature);
            if (!CRPoison_Started)
                StartCoroutine(PoisonDamage(poisonDamage));
            poisonDuration -= Time.deltaTime;
            if (poisonDuration <= 0)
            {
                poisoned = false;
                sprite.material = standardMaterial;
            }
        }
        if (slowed)
        {
            sprite.material = statusMaterials[(int)EffectInfo.EffectType.slow];
            speed = pursueSpeed * (slowPower / 100);
            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0)
            {
                slowed = false;
                sprite.material = standardMaterial;
                speed = pursueSpeed;
            }
        }
        if (frozen)
        {
            sprite.material = statusMaterials[(int)EffectInfo.EffectType.freeze];
            speed = 0;
            frozenDuration -= Time.deltaTime;
            if (frozenDuration <= 0)
            {
                frozen = false;
                sprite.material = standardMaterial;
                speed = pursueSpeed;
            }
        }
    }

    IEnumerator BurnDamage(DamageInfo burnDamage)
    {
        CRFire_Started = true;
        yield return new WaitForSeconds(2.0f);
        TakeDamage(burnDamage);
        CRFire_Started = false;
    }

    IEnumerator PoisonDamage(DamageInfo poisonDamage)
    {
        CRPoison_Started = true;
        yield return new WaitForSeconds(2.0f);
        TakeDamage(poisonDamage);
        CRFire_Started = false;
    }

    IEnumerator DeathEffect()
    {
        CRDeath_Started = true;
        isDead = true;
        anim.SetTrigger("Dead");
        Death.Play();
        GameObject manaObject = Instantiate(ManaPrefab, transform.position, Quaternion.identity);
        Mana mana = manaObject.GetComponent<Mana>();
        mana.value = manaQuantity;
        yield return new WaitForSeconds(4.0f);
        gameObject.SetActive(false);
    }

    protected void InitStatusVariables()
    {
        burned = false;
        poisoned = false;
        slowed = false;
        frozen = false;
    }
}