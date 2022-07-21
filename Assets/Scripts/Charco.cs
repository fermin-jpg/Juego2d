using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charco : MonoBehaviour
{
    public GameObject plantPrefab;
    bool plantAble = true;

    [Header("Effect Info")]
    public float duration;
    public float power;
    public EffectInfo.EffectType effect;

    EffectInfo effectInfo;

    private void Start()
    {
        Invoke("Disapear", 7);
        effectInfo = new EffectInfo(duration, power, effect);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "forest" && plantAble)
        {
            plantAble = false;
            Instantiate(plantPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity, transform.parent);
            Disapear();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemigos"))
            collision.gameObject.GetComponent<IEffects>().GetStatusEffect(effectInfo);
    }

    void Disapear()
    {
        GetComponent<Animator>().SetTrigger("Delete");
    }
    
    public void Delete()
    {
        Destroy(gameObject);
    }
    
}
