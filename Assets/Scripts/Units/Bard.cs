using UnityEngine;

public class Bard : AOEUnit {

    public float[] StunDuration = new float[4];
    public Animator weapon;
    public ParticleSystem particle;
    public override DamageType GetDamageType() {
        return DamageType.Emotional;
    }

    protected override void Setup() {
        base.Setup();
    }

    public override Debuff GetDebuff() {
        if (Tier[1] >= 3) {
            return new StunDebuff(StunDuration[Tier[1] - 1]);
        }
        return null;
    }

    protected override void Shoot() {
        weapon.SetTrigger("Play");
        particle.Play();
        base.Shoot();
    }
}
