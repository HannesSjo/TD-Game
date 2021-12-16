using UnityEngine;

[CreateAssetMenu(fileName = "MeleeUnitBoost", menuName = "StatBoost/Melee")]
public class StatBoostMelee : StatBoost {

    public int Damage = 1;
    public int CritChance = 10;
    public float AttackSpeed = 1;
    public bool ArmorPiercing = false;
    public override void ApplyBoost(Unit unit, int[] tier, int tree) {
        MeleeUnit pUnit = (MeleeUnit) unit;
        
        pUnit.Damage += Damage;
        pUnit.CritChance += CritChance;
        pUnit.AttackSpeed += AttackSpeed;
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

        base.ApplyBoost(unit, tier, tree);
    }
}
