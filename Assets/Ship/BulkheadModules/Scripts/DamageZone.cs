using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
   public void DealDamage(int damage)
    {
        HullModule mod;
        if(transform.parent.TryGetComponent(out mod))
        {
            Debug.Log("Took " + damage + " damage!");
            mod.Damage(damage, HullModule.DamageType.Impact);
        }
        
    }
}
