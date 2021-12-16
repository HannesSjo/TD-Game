using UnityEngine;

[System.Serializable]
public class UnitInfo {
    public Unit Unit;
    public Sprite SplashArt;
    public Sprite Sprite;

    public string Name;

    public int CoinsUsed;

    public int BaseCost;

    [System.Serializable]
    public class TreeCost {
        public int[] Costs;
    }

    public TreeCost[] TreeCosts;
}
