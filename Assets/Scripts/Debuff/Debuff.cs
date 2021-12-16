using UnityEngine;

public abstract class Debuff {
    protected Enemy Target;
    protected float Duration;

    public virtual void Start() {

    }

    public virtual void Update() {
        Duration -= Time.deltaTime;
        if(Duration <= 0) {
            End();
            Target.RemoveDebuff(this);
        }
    }

    public virtual void End() {

    }

    public void SetTarget(Enemy target) {
        Target = target;
    }

}
