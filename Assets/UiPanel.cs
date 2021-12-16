using UnityEngine;
using UnityEngine.UI;

public class UiPanel : MonoBehaviour {
    public UnitSelection Units;

    public UnitHolder[] UnitHolders;
    public UnitPlacement Placement;
    private CoinHandler coinHandler;

    private void Start() {
        coinHandler = FindObjectOfType<CoinHandler>();

        for (int i = 0; i < Units.Units.Length; i++) {
            UnitHolders[i].Img = Units.Units[i].UnitInfo.SplashArt;
            UnitHolders[i].Cost = Units.Units[i].UnitInfo.BaseCost;
            UnitHolders[i].Index = i;
        }
    }

    public void BuyUnit(int index) {
        int cost = Units.Units[index].UnitInfo.BaseCost;
        bool canAfford = coinHandler.CanAfford(cost);
        if (canAfford && FindObjectOfType<UnitPlacement>() == null) {
            UnitPlacement inst = GameObject.Instantiate(Placement);
            inst.UnitInfo = Units.Units[index].UnitInfo;
        }
    }
}
