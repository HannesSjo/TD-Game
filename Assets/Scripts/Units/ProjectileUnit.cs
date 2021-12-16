using System;
using UnityEngine;

public abstract class ProjectileUnit : Unit {

    public int Damage = 1;
    public int CritChance = 10;
    public float AttackSpeed = 1;
    public float ProjectileSpeed = 3;
    public int ProjectilePierces = 1;
    public bool ArmorPiercing = false;
    public bool CanShoot = false;
    public GameObject Projectile;

    private float timer = 0.0f;

    private TargetModes[] allowedTargetModes = {TargetModes.First, TargetModes.Last, TargetModes.Close, TargetModes.Strong};

    protected override void Update() {
        float attackCooldown = 1 / AttackSpeed;
        if (timer > attackCooldown && CanShoot && HasTarget()) {
            Shoot();
            timer = 0;
        }

        if (timer < attackCooldown + 1)
            timer += Time.deltaTime;

        base.Update();
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

    protected override void Shoot() {
        Enemy target = FindTarget();
        if (target == null)
            Shoot();
        LookAtTarget(target.transform.position);

        GameObject inst = (GameObject)Instantiate(Projectile, transform);
        bool isCrit = false;
        if (CritChance > UnityEngine.Random.Range(0, 100))
            isCrit = true;
        inst.GetComponent<Projectile>().SetProjectile(ProjectilePierces, ProjectileSpeed, Damage, isCrit, ArmorPiercing, target.transform.position, this);
    }

    public override string GetStatString() {
        string statString = "";
        statString += "Damage " + Damage + "\n";
        statString += "Range " + Range + " Tiles\n";
        statString += "Critical chance " + CritChance + "%\n";
        statString += "Attack speed " + AttackSpeed + "/s\n";
        statString += "Projectile speed " + ProjectileSpeed + "\n";
        statString += "Projectile Pierces " + ProjectilePierces + "\n";
        return statString;
    }

    public override TargetModes[] GetTargetModes() {
        return allowedTargetModes;
    }
}
