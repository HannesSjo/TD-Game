using UnityEngine;

public class Archer : ProjectileUnit {
    public DamageType DamageType = DamageType.Physical;

    public Sprite[] BowSprites;
    public SpriteRenderer Bow;
    private float bowTimer = 0;

    public int[] FireDamage;
    public float[] FireTicks;
    public float[] DefenceReduction;
    public float[] BurnTime;

    public override DamageType GetDamageType() {
        return DamageType;
    }

    protected override void Update() {
        if (bowTimer <= 0) {
            Bow.sprite = BowSprites[1];
        }
        else {
            bowTimer -= Time.deltaTime;
        }

        base.Update();
    }

    protected override void Shoot() {
        Bow.sprite = BowSprites[0];
        bowTimer = (1 / AttackSpeed) / 2;
        base.Shoot();
    }

    public override Debuff GetDebuff() {
        if (Tier[0] > 0) {
            int lvl = Tier[0]-1;
            return new FireDebuff(BurnTime[lvl], FireDamage[lvl], FireTicks[lvl], DefenceReduction[lvl]);
        }
        return null;
    }



}
