using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeman : MeleeUnit
{
    public override DamageType GetDamageType() {
        return DamageType.Physical;
    }

    public override Debuff GetDebuff() {
        return null;
    }
}
