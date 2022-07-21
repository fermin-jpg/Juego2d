using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInfo
{
    public enum EffectType
    {
        poison,
        slow,
        burn,
        freeze,
        none
    }

    public EffectInfo(float duration, float power, EffectType type)
    {
        this.duration = duration;
        this.power = power;
        this.type = type;
    }

    public EffectType type;
    public float duration;
    public float power;
}
