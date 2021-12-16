using UnityEngine;

public class Tile : MonoBehaviour {

    public bool Placable = false;
    public Unit Unit;
    private CoinHandler coinHandler;

    private TowerPanel towerPanel;
    private void Start() {
        coinHandler = FindObjectOfType<CoinHandler>();
        towerPanel = FindObjectOfType<TowerPanel>();
    }

    public void PlaceUnit(Unit unit) {
        coinHandler.Purchase(unit.UnitInfo.BaseCost);
        Placable = false;
        GameObject inst = Instantiate(unit.gameObject, transform);

        Unit = inst.GetComponent<Unit>();
    }

    public void RemoveUnit() {
        Destroy(Unit.gameObject);
        Placable = true;
        Unit = null;
    }

    private void OnMouseUp() {
        if (Unit != null) {
            towerPanel.DisplayUnit(Unit.UnitInfo, this);
        }
        else {
            //towerPanel.CloseDisplay();
        }
    }
}
