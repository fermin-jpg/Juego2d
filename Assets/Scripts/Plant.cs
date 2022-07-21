using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject plantBulletPrefab;
    Animator _animator;
    float timer;
    float cooldownShoot = 1.6f;

    bool shootAble;

    void Start()
    {
        _animator = GetComponent<Animator>();
        timer = cooldownShoot;
        shootAble = true;
        Invoke("Death", 6);
    }

    void Death()
    {
        Destroy(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            shootAble = true;
            timer = cooldownShoot;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 && shootAble)
        {
            GameObject b = Instantiate(plantBulletPrefab, transform.position, Quaternion.identity, transform);
            b.GetComponent<PlantBullet>().StartShoot(collision.transform.position);
            //Debug.Log("Disparo");
            shootAble = false;
        }
    }
}
