using UnityEngine;

public class Mage : ProjectileUnit {
    public DamageType DamageType = DamageType.Physical;

    public int[] FrostDamage;
    public float[] FrostTicks;
    public float[] SlowAmount;
    public float[] FrostDuration;

    public override DamageType GetDamageType() {
        return DamageType;
    }

    public override Debuff GetDebuff() {
        if (Tier[1] > 0) {
            int lvl = Tier[1] - 1;
            return new FrostDebuff(FrostDuration[lvl], SlowAmount[lvl], FrostDamage[lvl], FrostTicks[lvl]);
        }
        return null;
    }
}
