using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour{
    public Vector2 RightPos;
    public Vector2 LeftPos;
    public float SellReturn = 0.5f;
    private RectTransform rTransform;

    public GameObject Display;
    [Space(5)]
    public Text Name;
    public Text SubName;
    public Image Image;
    [Space(5)]
    public GameObject TargetStyle;
    public Text TargetStyleText;
    public Button TargetStyleRightButton;
    public Button TargetStyleLeftButton;
    [Space(5)]
    public Button Tree1;
    public Text Tree1Text;
    public Text Tree1CostText;
    public GameObject[] Tree1Dots;
    public Button Tree2;
    public Text Tree2Text;
    public Text Tree2CostText;
    public GameObject[] Tree2Dots;
    private UnitInfo unit;
    private Tile tile;

    private Image img;
    private CoinHandler coinHandler;
    private void Start() {
        img = GetComponent<Image>();
        rTransform = GetComponent<RectTransform>();
        coinHandler = FindObjectOfType<CoinHandler>();
    }

    private void Update() {
        if (unit != null) {
            Display.SetActive(true);

            int choosenTree = -1;
            if (unit.Unit.Tier[0] > unit.Unit.MaxSecondaryTreeRank)
                choosenTree = 0;
            if (unit.Unit.Tier[1] > unit.Unit.MaxSecondaryTreeRank)
                choosenTree = 1;

            bool inter = false;
            if (unit.Unit.Tier[0] < unit.Unit.Upgrades[0].StatBoosts.Length) {
                if (choosenTree != 1 || unit.Unit.Tier[0] < unit.Unit.MaxSecondaryTreeRank) {
                    if (coinHandler.CanAfford(unit.TreeCosts[0].Costs[unit.Unit.Tier[0]])) {
                        inter = true;
                    }
                }
            }
            Tree1.interactable = inter;

            inter = false;
            if (unit.Unit.Tier[1] < unit.Unit.Upgrades[1].StatBoosts.Length) {
                if (choosenTree != 0 || unit.Unit.Tier[1] < unit.Unit.MaxSecondaryTreeRank) {
                    if (coinHandler.CanAfford(unit.TreeCosts[1].Costs[unit.Unit.Tier[1]])) {
                        inter = true;
                    }
                }
            }
            Tree2.interactable = inter;


        } else {
            Display.SetActive(false);
        }
    }

    public void CloseDisplay() {
        if (unit != null)
            unit.Unit.DisplayRange(false);
        unit = null;
    }

    private void UpdateDisplay() {
        DisplayUnit(unit, tile);
    }

    public void Sell() {
        coinHandler.GainCoins(Mathf.RoundToInt(unit.CoinsUsed * SellReturn));
        tile.RemoveUnit();
        CloseDisplay();
    }

    public void Upgrade(int tree) {
        int cost = unit.TreeCosts[tree - 1].Costs[unit.Unit.Tier[tree-1]];
        
        if (coinHandler.CanAfford(cost)) {
            coinHandler.Purchase(cost);
            unit.Unit.Upgrade(tree);
            DisplayUnit(unit, tile);
        }
        else {
            Debug.Log("Cant afford");
        }
    }

    public void TargetSelect(int direction) {
        TargetModes[] targetModes = unit.Unit.GetTargetModes();
        TargetModes curTargetMode = unit.Unit.GetTargetMode();

        int index = Array.IndexOf(targetModes, curTargetMode);
        if (index > -1) {
            int newIndex = index + direction;
            if (newIndex < targetModes.Length && newIndex >= 0) {
                unit.Unit.SetTargetMode(targetModes[newIndex]);
                UpdateDisplay();
            }
        }
    }

    public void DisplayUnit (UnitInfo displayUnit, Tile tile) {
        if (unit != null)
            unit.Unit.DisplayRange(false);

        this.tile = tile;
        unit = displayUnit;
        if (displayUnit.Unit.transform.position.x <= 0) {
            rTransform.anchoredPosition = RightPos;
        } else {
            rTransform.anchoredPosition = LeftPos;
        }

        Name.text = displayUnit.Name;

        string subname = "";

        if (displayUnit.Unit.Tier[0] > 0 || displayUnit.Unit.Tier[1] > 0) {
            if (displayUnit.Unit.Tier[0] >= displayUnit.Unit.Tier[1]) {
                subname = displayUnit.Unit.Upgrades[0].StatBoosts[displayUnit.Unit.Tier[0]-1].UpgradeName;
            }
            else {
                subname = displayUnit.Unit.Upgrades[1].StatBoosts[displayUnit.Unit.Tier[1]-1].UpgradeName;
            }
        }

        SubName.text = subname;
        Image.sprite = displayUnit.SplashArt;


        if (unit.Unit.GetTargetModes() != null) {
            TargetStyle.SetActive(true);
            TargetStyleText.text = Enum.GetName(typeof(TargetModes), unit.Unit.GetTargetMode());

            TargetModes[] targetModes = unit.Unit.GetTargetModes();

            int index = Array.IndexOf(targetModes, unit.Unit.GetTargetMode());
            if (index > -1) {
                if (index + 1 < targetModes.Length && index + 1 >= 0) {
                    TargetStyleRightButton.interactable = true;
                } else {
                    TargetStyleRightButton.interactable = false;
                }

                if (index - 1 < targetModes.Length && index - 1 >= 0) {
                    TargetStyleLeftButton.interactable = true;
                } else {
                    TargetStyleLeftButton.interactable = false;
                }
            }
        }
        else {
            TargetStyle.SetActive(false);
        }



        int choosenTree = -1;
        if (displayUnit.Unit.Tier[0] > displayUnit.Unit.MaxSecondaryTreeRank)
            choosenTree = 0;
        if (displayUnit.Unit.Tier[1] > displayUnit.Unit.MaxSecondaryTreeRank)
            choosenTree = 1;

        if (displayUnit.Unit.Tier[0] < displayUnit.Unit.Upgrades[0].StatBoosts.Length) {
            if (choosenTree != 1 || displayUnit.Unit.Tier[0] < displayUnit.Unit.MaxSecondaryTreeRank) {
                Tree1Text.text = displayUnit.Unit.Upgrades[0].StatBoosts[displayUnit.Unit.Tier[0]].UpgradeName;
                Tree1CostText.text = displayUnit.TreeCosts[0].Costs[displayUnit.Unit.Tier[0]] + "c";
            } else {
                Tree1Text.text = "Maxed";
                Tree1CostText.text = "";
            }
        }
        else {
            Tree1Text.text = "Maxed";
            Tree1CostText.text = "";
        }

        foreach (GameObject dot in Tree1Dots) {
            dot.SetActive(true);
        }

        for (int i = 0; i < displayUnit.Unit.Tier[0]; i++) {
            Tree1Dots[i].SetActive(false);
        }

        if (displayUnit.Unit.Tier[1] < displayUnit.Unit.Upgrades[1].StatBoosts.Length) {
            if (choosenTree != 0 || displayUnit.Unit.Tier[1] < displayUnit.Unit.MaxSecondaryTreeRank) {
                Tree2Text.text = displayUnit.Unit.Upgrades[1].StatBoosts[displayUnit.Unit.Tier[1]].UpgradeName;
                Tree2CostText.text = displayUnit.TreeCosts[1].Costs[displayUnit.Unit.Tier[1]] + "c";
            }
            else {
                Tree2Text.text = "Maxed";
                Tree2CostText.text = "";
            }
        } else {
            Tree2Text.text = "Maxed";
            Tree2CostText.text = "";
        }

        foreach (GameObject dot in Tree2Dots) {
            dot.SetActive(true);
        }

        for (int i = 0; i < displayUnit.Unit.Tier[1]; i++) {
            Tree2Dots[i].SetActive(false);
        }

        unit.Unit.DisplayRange(true);
    }
}
