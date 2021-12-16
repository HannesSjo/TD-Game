using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileUnitBoost", menuName = "StatBoost/Projectile")]
public class StatBoostProjectile : StatBoost {

    public int Damage = 0;
    public int Range = 0;
    public int CritChance = 0;
    public float AttackSpeed = 0;
    public float ProjectileSpeed = 0;
    public int ProjectilePierces = 0;
    public bool ArmorPiercing = false;

    public GameObject Projectile;
    public override void ApplyBoost(Unit unit, int[] tier, int tree) {
        ProjectileUnit pUnit = (ProjectileUnit) unit;
        
        pUnit.Damage += Damage;
        pUnit.Range += Range;
        pUnit.CritChance += CritChance;
        pUnit.AttackSpeed += AttackSpeed;
        pUnit.ProjectileSpeed += ProjectileSpeed;
        pUnit.ProjectilePierces += ProjectilePierces;
        if (ArmorPiercing) {
            pUnit.ArmorPiercing = ArmorPiercing;
        }
        if (UnitSprite != null) {
            int tree2 = 2;
            if (tree > 1) {
                tree2 = 1;
            }

            if (tier[tree - 1] + 1 > tier[tree2 - 1]) {
                unit.SpriteRenderer.sprite = UnitSprite;
            }
        }
        if (Projectile != null)
            pUnit.Projectile = Projectile;

        base.ApplyBoost(unit, tier, tree);
    }
}
