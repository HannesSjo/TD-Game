using UnityEngine;

[CreateAssetMenu(fileName = "AOEUnitBoost", menuName = "StatBoost/AOE")]
public class StatBoostAOE : StatBoost {

    public int Damage = 0;
    public int Range = 0;
    public int CritChance = 0;
    public float AttackSpeed = 0;
    public bool ArmorPiercing = false;
    public override void ApplyBoost(Unit unit, int[] tier, int tree) {
        AOEUnit aoeUnit = (AOEUnit) unit;
        
        aoeUnit.Damage += Damage;
        aoeUnit.Range += Range;
        aoeUnit.CritChance += CritChance;
        aoeUnit.AttackSpeed += AttackSpeed;
        if (ArmorPiercing) {
            aoeUnit.ArmorPiercing = ArmorPiercing;
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
