using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
   public bool DealDamage(int damage)
    {
        HullModule mod;
        if(transform.parent.TryGetComponent(out mod) && mod.health > 0)
        {
            mod.Damage(damage, HullModule.DamageType.Impact);
            return true;
        }
        return false;
    }
}
