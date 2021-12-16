using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeUnit : Unit {

    public int Damage = 1;
    public int CritChance = 10;
    public float AttackSpeed = 1;
    public bool ArmorPiercing = false;

    public MeleeWeapon Weapon;
    public GameObject WeaponHolder;

    private TargetModes[] allowedModes = new TargetModes[] { TargetModes.First, TargetModes.Last, TargetModes.Close, TargetModes.Strong };
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

        Enemy target = FindTarget();

        AimOnTarget(target.transform.position);
        Weapon.Attack();
    }

    private void AimOnTarget(Vector2 target) {
        Vector2 dir = target - (Vector2) transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        WeaponHolder.transform.rotation = q;
    }

    protected Enemy FindTarget() {
        if (TargetMode == TargetModes.First)
            Enemys.Sort((e1, e2) => e2.DistanceTravaled.CompareTo(e1.DistanceTravaled));
        else if (TargetMode == TargetModes.Close)
            Enemys.Sort((e1, e2) => Vector2.Distance(transform.position, e1.transform.position).CompareTo(Vector2.Distance(transform.position, e2.transform.position)));
        else if (TargetMode == TargetModes.Last)
            Enemys.Sort((e1, e2) => e1.DistanceTravaled.CompareTo(e2.DistanceTravaled));
        else if (TargetMode == TargetModes.Strong)
            Enemys.Sort((e1, e2) => e2.Tier.CompareTo(e1.Tier));
        return Enemys[0];
    }

    public override TargetModes[] GetTargetModes() {
        return allowedModes;
    }
}
