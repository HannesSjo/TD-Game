using UnityEngine;

//Holds stat buffs for each upgrade
//0 means no change

//[CreateAssetMenu(fileName = "StatBoost", menuName = "StatBoost")]
public abstract class StatBoost : ScriptableObject {
    public string UpgradeName;
    public string UpgradeDescription;

    public Sprite UnitSprite;

    public virtual void ApplyBoost (Unit unit, int[] tier, int tree) {
        
    }
}
