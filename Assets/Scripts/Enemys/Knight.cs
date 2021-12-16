using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Knight : Enemy {

    public List<DamageType> Imunities;

    public override void TakeDamage(int amount, bool armorPiercing, bool crit, DamageType damageType) {
        if (!Imunities.Contains(damageType))
            base.TakeDamage(amount, armorPiercing, crit, damageType);
    }
}
