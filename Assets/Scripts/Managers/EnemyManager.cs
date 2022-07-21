using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] enemies;

    public float distanceToActivate;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToPlayer();
    }

    public void CheckDistanceToPlayer()
    {
        for(int i=0;i<enemies.Length;i++)
        {
            if (Vector3.Distance(enemies[i].position, player.position) < distanceToActivate)
            {
                if(enemies[i].gameObject.GetComponent<Enemies>().isDead == false)
                    enemies[i].gameObject.SetActive(true);
            }
            else
            {
                enemies[i].gameObject.SetActive(false);
            }
        }
    }
}
