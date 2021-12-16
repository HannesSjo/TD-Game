using UnityEngine;

public class StunDebuff : Debuff {
    public StunDebuff(float duration) {
        this.Duration = duration;
    }
    public override void Start() {
        Target.Stunned = true;
        base.Start();
    }
    public override void End() {
        Target.Stunned = false;
        base.End();
    }
}
