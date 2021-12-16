using System;
using UnityEngine;

public class Bonfire : AOEUnit {
    [System.Serializable]
    public class BonfireStats {
        public int[] FireDamage;
        public float[] FireTicks;
        public float[] DefenceReduction;
        public float[] BurnTime;
    }
    public BonfireStats Base;
    public BonfireStats[] Stats = new BonfireStats[2];

    public ParticleSystem particle;
    public override DamageType GetDamageType() {
        return DamageType.Physical;
    }

    protected override void Setup() {
        base.Setup();
    }

    public override Debuff GetDebuff() {
        BonfireStats def = new BonfireStats();

        def.BurnTime = new float[] { Base.BurnTime[0] };
        def.FireTicks = new float[] { Base.FireTicks[0] };
        def.DefenceReduction = new float[] { Base.DefenceReduction[0] };
        def.FireDamage = new int[] { Base.FireDamage[0] };

        if (Tier[0] > 0) {
            def.FireDamage[0] += Stats[0].FireDamage[Tier[0] - 1];
            def.FireTicks[0] += Stats[0].FireTicks[Tier[0] - 1];
            def.DefenceReduction[0] += Stats[0].DefenceReduction[Tier[0] - 1];
            def.BurnTime[0] += Stats[0].BurnTime[Tier[0] - 1];
        }
        if (Tier[1] > 0) {
            def.FireDamage[0] += Stats[1].FireDamage[Tier[1] - 1];
            def.FireTicks[0] += Stats[1].FireTicks[Tier[1] - 1];
            def.DefenceReduction[0] += Stats[1].DefenceReduction[Tier[1] - 1];
            def.BurnTime[0] += Stats[1].BurnTime[Tier[1] - 1];
        }
        return new FireDebuff(def.BurnTime[0], Base.FireDamage[0], Base.FireTicks[0], Base.DefenceReduction[0]);
    }

    protected override void Shoot() {
        particle.Play();
        base.Shoot();
    }
}
