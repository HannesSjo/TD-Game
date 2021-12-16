using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AOEUnit : Unit {

    public int Damage = 1;
    public int CritChance = 10;
    public float AttackSpeed = 1;
    public bool ArmorPiercing = false;

    private float timer = 0f;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        float attackCooldown = 1 / AttackSpeed;
        if (timer > attackCooldown && HasTarget()) {
            Shoot();
            timer = 0f;
        }

        if (timer < attackCooldown + 1)
            timer += Time.deltaTime;

        base.Update();
    }

    protected override void Shoot() {
        try {
            Vector2 target = Vector2.zero;
            Enemy[] enemys = Enemys.ToArray();

            for (int i = 0; i < enemys.Length; i++) {
                if (enemys[i] != null) {
                    target += (Vector2) enemys[i].transform.position;
                    bool isCrit = false;
                    if (CritChance > Random.Range(0, 100))
                        isCrit = true;
                    Debuff debuff = GetDebuff();
                    if (debuff != null) {
                        debuff.SetTarget(enemys[i]);
                        enemys[i].AddDebuff(debuff);
                    }
                    enemys[i].TakeDamage(Damage, ArmorPiercing, isCrit, GetDamageType());
                }
            }
            LookAtTarget(target / enemys.Length);
        }
        catch(System.Exception x) {
            Debug.Log(x.Message);
        }
    }

    public override TargetModes[] GetTargetModes() {
        return null;
    }
}
