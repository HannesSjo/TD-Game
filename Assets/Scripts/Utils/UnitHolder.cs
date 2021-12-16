using UnityEngine;
using UnityEngine.UI;

public class UnitHolder : MonoBehaviour {
    private UiPanel panel;
    private Image img;
    private Text cost;

    public int Index;

    public Sprite Img {
        set {
            img.sprite = value;
        }
    }

    public int Cost {
        set {
            cost.text = value + "g";
        }
    }
    private void Awake() {
        panel = GetComponentInParent<UiPanel>();
        img = GetComponent<Image>();
        cost = GetComponentInChildren<Text>();
    }

    private void Start() {
        GetComponent<Button>().onClick.AddListener(delegate { panel.BuyUnit(Index); });
    }
}
