using System;
using System.Collections.Generic;
using UnityEngine;

public enum TargetModes {
    None,
    First,
    Close,
    Last,
    Strong,
    Most,
    Right,
    Left
}

public abstract class Unit : MonoBehaviour {

    public UnitInfo UnitInfo;

    public int Range = 3;

    [System.Serializable]
    public struct UpgradeStats {
        [SerializeField]
        public StatBoost[] StatBoosts;
    }

    public UpgradeStats[] Upgrades;

    public SpriteRenderer SpriteRenderer;

    public TargetModes TargetMode = TargetModes.None;

    private GameObject rangeIndicator;

    public int[] Tier;
    public int MaxSecondaryTreeRank = 2;

    protected List<Enemy> Enemys;

    protected virtual void Start() {
        Enemys = new List<Enemy>();
        rangeIndicator = this.transform.Find("RangeIndicator").gameObject;
        Tier = new int[] { 0, 0 };
        UnitInfo.CoinsUsed = UnitInfo.BaseCost;
        TargetMode = GetTargetModes()[0];
        DisplayRange(false);
        Setup();
    }

    protected virtual void Setup() {
        rangeIndicator.transform.localScale = new Vector2((Range + 0.5f) * 2, (Range + 0.5f) * 2);
        GetComponent<BoxCollider2D>().size = new Vector2((Range + 0.5f) * 2, (Range + 0.5f) * 2);
    }

    protected virtual void Update() {

    }

    public void Upgrade (int tree) {
        //return if tree is unvalid
        if (tree > 2 || tree < 1) {
            ErrorMsg(tree + " out of range of " + Tier);
            return;
        }
        //return if Max tier reached
        if (Tier[tree-1] + 1 > Upgrades[tree - 1].StatBoosts.Length) {
            ErrorMsg("Max Level reached in tree " + tree);
            return;
        }
        int choosenTree = -1;
        if (Tier[0] > MaxSecondaryTreeRank)
            choosenTree = 0;
        if (Tier[1] > MaxSecondaryTreeRank)
            choosenTree = 1;
        if (Tier[tree - 1] + 1 > MaxSecondaryTreeRank) {
            if (choosenTree == -1 || choosenTree == tree - 1) {
                Upgrades[tree - 1].StatBoosts[Tier[tree - 1]].ApplyBoost(this, Tier, tree);
                UnitInfo.CoinsUsed += UnitInfo.TreeCosts[tree - 1].Costs[Tier[tree - 1]];
                Tier[tree - 1]++;
            }
            else {
                ErrorMsg("Max Level in secondary tree " + tree);
            }
        }
        else {
            Upgrades[tree - 1].StatBoosts[Tier[tree - 1]].ApplyBoost(this, Tier, tree);
            Tier[tree - 1]++;
        }
        Setup();
    }

    protected void ErrorMsg(string msg) {
        Debug.Log(msg);
    }

    protected void LookAtTarget(Vector2 target) {
        Vector3 scale = transform.localScale;
        if (target.x < transform.position.x) {
            if (scale.x > 0) {
                scale.x *= -1;
            }
        } else if (target.x > transform.position.x) {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    protected abstract void Shoot();

    public abstract TargetModes[] GetTargetModes();

    public TargetModes GetTargetMode() {
        return TargetMode;
    }

    public void SetTargetMode(TargetModes mode) {
        if (Array.IndexOf(GetTargetModes(), mode) > -1) {
            TargetMode = mode;
        }
    }

    protected bool HasTarget() {
        if (Enemys.Count > 0)
            return true;
        else
            return false;
    }

    protected void DisplayRangeIndicator(bool display) {
        rangeIndicator.SetActive(display);
    }

    public abstract Debuff GetDebuff();

    public abstract DamageType GetDamageType();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Enemys.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Enemys.Remove(collision.GetComponent<Enemy>());
        }
    }

    public virtual string GetStatString() {
        return null;
    }

    public void DisplayRange(bool value) {
        rangeIndicator.SetActive(value);
    }
}
