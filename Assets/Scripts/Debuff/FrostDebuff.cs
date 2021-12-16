using UnityEngine;

public class FrostDebuff : Debuff {
    public DamageType DamageType = DamageType.Magical;

    private int damage;
    private float ticks;
    private float slowAmount;

    private float timer;
    public FrostDebuff(float duration, float slowAmount, int damage, float ticks) {
        this.Duration = duration;
        this.damage = damage;
        this.ticks = ticks;
        this.slowAmount = slowAmount;
    }
    public override void Start() {
        Target.Slow = (slowAmount *-1);
        base.Start();
    }
    public override void Update() {
        if (damage > 0) {
            float cd = 1 / ticks;
            if (timer > cd) {
                Target.TakeDamage(damage, true, false, DamageType);
                timer = 0;
            }
            if (timer < cd + 1)
                timer += Time.deltaTime;
        }

        base.Update();
    }
    public override void End() {
        Target.Slow = slowAmount;
        base.End();
    }
}
