using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public enum DamageEffectiveness
    {
        resisted,
        normal,
        vulnerable
    }

    public enum DamageType
    {
        physical,
        fire,
        water,
        nature
    }

    public DamageInfo(float amount, DamageType type)
    {
        this.amount = amount;
        this.type = type;
    }

    public float amount;
    public DamageType type;
}
