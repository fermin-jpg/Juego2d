using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    float Health { get; set; }
    void TakeDamage(DamageInfo damageInfo);
}
