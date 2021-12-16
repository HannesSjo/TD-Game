using UnityEngine;

public class Ninja : ProjectileUnit {
    public DamageType DamageType = DamageType.Physical;
    public float[] StunDuration;
    public int[] BombChance;

    public GameObject Bomb;

    private bool bomb = true;
    public override DamageType GetDamageType() {
        return DamageType;
    }

    public override Debuff GetDebuff() {
        if (Tier[1] >= 3 && bomb) {
            int lvl = Tier[1] - 1;
            return new StunDebuff(StunDuration[lvl]);
        }
        return null;
    }

    protected override void Shoot() {
        if (Tier[1] < 3 || BombChance[Tier[1] - 1] <= Random.Range(0, 100)) {
            bomb = false;
            base.Shoot();
        }
        else {
            bomb = true;
            Enemy target = FindTarget();
            if (target == null)
                Shoot();
            LookAtTarget(target.transform.position);

            GameObject inst = (GameObject)Instantiate(Bomb, transform);
            bool isCrit = false;
            if (CritChance > Random.Range(0, 100))
                isCrit = true;
            inst.GetComponent<Projectile>().SetProjectile(0 , ProjectileSpeed / 2, Damage, isCrit, ArmorPiercing, target.transform.position, this);
        }
    }
}
