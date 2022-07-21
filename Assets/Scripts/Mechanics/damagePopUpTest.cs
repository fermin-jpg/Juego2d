using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePopUpTest : MonoBehaviour
{
    public Transform boticario;
    public Transform cazador;

    DamageInfo damageinfophysic;
    DamageInfo damageinfofire;
    DamageInfo damageinfonature;
    DamageInfo damageinfowater;

    EffectInfo slowEffect;
    EffectInfo freezeEffect;

    IDamage boticario2;
    IDamage cazador2;

    IEffects boticario1;
    IEffects boticario3;

    void Start()
    {
        boticario2 = boticario.GetComponent<IDamage>();
        cazador2 = cazador.GetComponent<IDamage>();

        boticario1 = boticario.GetComponent<IEffects>();
        boticario3 = cazador.GetComponent<IEffects>();

        damageinfophysic = new DamageInfo(20,DamageInfo.DamageType.physical);
        damageinfofire = new DamageInfo(20, DamageInfo.DamageType.fire);
        damageinfonature = new DamageInfo(20, DamageInfo.DamageType.nature);
        damageinfowater = new DamageInfo(20, DamageInfo.DamageType.water);

        slowEffect = new EffectInfo(6.0f, 5.0f, EffectInfo.EffectType.slow);
        freezeEffect = new EffectInfo(5.0f, 10.0f, EffectInfo.EffectType.freeze);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            boticario1.GetStatusEffect(slowEffect);
            boticario3.GetStatusEffect(freezeEffect);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            boticario2.TakeDamage(damageinfophysic);
            cazador2.TakeDamage(damageinfophysic);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            boticario2.TakeDamage(damageinfofire);
            cazador2.TakeDamage(damageinfofire);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            boticario2.TakeDamage(damageinfonature);
            cazador2.TakeDamage(damageinfonature);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            boticario2.TakeDamage(damageinfowater);
            cazador2.TakeDamage(damageinfowater);
        }
    }
}
