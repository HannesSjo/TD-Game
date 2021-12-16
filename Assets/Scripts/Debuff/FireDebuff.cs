using UnityEngine;

public class FireDebuff : Debuff {
    DamageType DamageType = DamageType.Physical;

    private int damage;
    private float defenceReduction;
    private float ticks;

    private float timer;
    public FireDebuff(float duration, int damage, float ticks, float defenceReduction = 0f) {
        this.Duration = duration;
        this.damage = damage;
        this.ticks = ticks;
        this.defenceReduction = defenceReduction;
    }
    public override void Start() {
        Target.DefenceReduction += defenceReduction;
        base.Start();
    }
    public override void Update() {

        float cd = 1 / ticks;
        if (timer > cd) {
            Target.TakeDamage(damage, true, false, DamageType.Physical);
            timer = 0;
        }
        if (timer < cd + 1)
            timer += Time.deltaTime;

        base.Update();
    }
    public override void End() {
        Target.DefenceReduction -= defenceReduction;
        base.End();
    }
}
