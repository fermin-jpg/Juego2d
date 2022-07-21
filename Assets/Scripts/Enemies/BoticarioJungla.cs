using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoticarioJungla : Enemies, IDamage
{
    protected const int MAX_HEALTH = 70;

    public GameObject firePotionPrefab;
    public GameObject slowPotionPrefab;

    public AudioSource Throw;

    float attackCooldown;
    bool firePotion;
    const float MAX_ATTACKCOOLDOWN = 2.0f;

    enum State
    {
        patrol,
        attack,
        pursue
    }

    State currentState;
    State nextState;

    public override void Start()
    {
        base.Start();
        combatDistance = 10.0f;
        pursueDistance = 20.0f;
        firePotion = true;


        currentState = State.patrol;
        nextState = State.patrol;
        Health = MAX_HEALTH;
        currentTarget = pointR.position;
        attackCooldown = 0;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pursueDistance);
    }
    public override void Movement()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetBool("inCombat"))
        {
            return;
        }
        base.Movement();
    }

    public override void Update()
    {
        base.Update();

        if (frozen)
            anim.SetBool("Frozen", true);
        else
            anim.SetBool("Frozen", false);

        healthBar.SetHealth(Health, MAX_HEALTH);

        //Change States
        if (distanceToPlayer < combatDistance && !isDead)
        {
            nextState = State.attack;
        }
        if (distanceToPlayer > pursueDistance && !isDead)
        {
            nextState = State.patrol;
        }
        if (distanceToPlayer > combatDistance && distanceToPlayer <= pursueDistance && !isDead)
        {
            nextState = State.pursue;
        }
        //States
        if (currentState == State.patrol || currentState == State.pursue)
        {
            if (!recentlyHit)
                Movement();
        }
        if (currentState == State.attack)
        {
            if (attackCooldown <= 0)
            {
                anim.SetTrigger("Attack");
                Throw.Play();
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    return;
                }
                attackCooldown = MAX_ATTACKCOOLDOWN;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
                anim.SetTrigger("Idle");
            }
        }

        if (currentState != nextState)
        {
            if (nextState == State.attack)
            {
                currentTarget = Player.position;
                Vector3 direction = Player.transform.localPosition - transform.localPosition;
                if (direction.x > 0)
                {
                    sprite.flipX = false;
                }
                else if (direction.x < 0)
                {
                    sprite.flipX = true;
                }
                anim.SetBool("inCombat", true);
            }
            if (nextState == State.pursue)
            {
                currentTarget = Player.position;
                speed = pursueSpeed;
            }
            if (nextState == State.patrol)
            {
                if (Vector3.Distance(transform.position, pointR.position) > Vector3.Distance(transform.position, pointL.position))
                    currentTarget = pointL.position;
                else
                    currentTarget = pointR.position;
                anim.SetBool("inCombat", false);
                speed = patrolSpeed;
            }
            currentState = nextState;
        }
    }

    public override void Attack()
    {
        if (firePotion)
        {
            if (facingRight)
            {
                Instantiate(firePotionPrefab, transform.position + transform.TransformDirection(transform.right), Quaternion.identity);
                firePotion = !firePotion;
            }
            else
            {
                Instantiate(firePotionPrefab, transform.position + transform.TransformDirection(-transform.right), Quaternion.identity);
                firePotion = !firePotion;
            }

        }
        else
        {
            if (facingRight)
            {
                Instantiate(slowPotionPrefab, transform.position + transform.TransformDirection(transform.right), Quaternion.identity);
                firePotion = !firePotion;
            }
            else
            {
                Instantiate(slowPotionPrefab, transform.position + transform.TransformDirection(-transform.right), Quaternion.identity);
                firePotion = !firePotion;
            }
        }

    }

    public override void TakeDamage(DamageInfo damageinfo)
    {
        base.TakeDamage(damageinfo);
    }

    protected override void Die()
    {
        base.Die();
    }
}