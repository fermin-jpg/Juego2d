using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cazador : Enemies
{
    public const int MAX_HEALTH = 90;

    public GameObject bulletPrefab;

    public AudioSource Alert;
    public AudioSource Shoot;

    //Estados

    enum State
    {
        patrol,
        pursue,
        attack
    }

    State currentState;
    State nextState;

    Vector3 instantiateOffset;

    public override void Movement()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetBool("inCombat"))
        {
            return;
        }
        base.Movement();
    }

    public override void Start()
    {
        combatDistance = 15.0f;
        pursueDistance = 25.0f;
        instantiateOffset = new Vector3();
        Health = MAX_HEALTH;
        base.Start();
        currentTarget = pointR.position;
        facingRight = true;
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
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                return;
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
            anim.SetTrigger("Shoot");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            {
                return;
            }
            Vector3 direction = Player.transform.localPosition - transform.localPosition;
            if (direction.x > 0)
            {
                sprite.flipX = false;
            }
            else if (direction.x < 0)
            {
                sprite.flipX = true;
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
                anim.SetTrigger("Idle");
            }
            if (nextState == State.pursue)
            {
                Alert.Play();
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
    public void RangeAttack()
    {
        if (sprite.flipX == false)
        {
            instantiateOffset = transform.TransformDirection(transform.right);
        }
        else
            instantiateOffset = transform.TransformDirection(-transform.right);
        GameObject bullet = Instantiate(bulletPrefab, transform.position + instantiateOffset, Quaternion.identity);
        bullet.GetComponent<Bullet>().Fire(transform.TransformDirection(Player.transform.localPosition - transform.localPosition), sprite.flipX);
    }

    public override void TakeDamage(DamageInfo damageinfo)
    {
        base.TakeDamage(damageinfo);
    }
}
