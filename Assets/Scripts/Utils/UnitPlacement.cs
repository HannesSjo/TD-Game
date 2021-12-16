using UnityEngine;
using UnityEngine.UI;

public class UnitPlacement : MonoBehaviour {

    public UnitInfo UnitInfo;
    public float speed = 1f;
    public RectTransform Thrashcan;
    public float Thrashrange;

    private SpriteRenderer spriteRenderer;
    private TowerPanel towerPanel;
    private Tile tile;

    private void Start() {
        if (UnitInfo == null) 
            Destroy(gameObject);
        Thrashcan = GameObject.Find("Thrashcan").GetComponent<RectTransform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        towerPanel = FindObjectOfType<TowerPanel>();
        towerPanel.CloseDisplay();
        spriteRenderer.sprite = UnitInfo.Sprite;
    }

    private void Update() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = new Vector2(worldPosition.x.RoundToFive() , worldPosition.y.RoundToFive());
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (tile != null) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Thrashcan.position);
            if (Vector2.Distance(pos, transform.position) <= Thrashrange) {
                Destroy(gameObject);
            } else if (tile.Placable) {
                if (Input.GetMouseButtonUp(0)) {
                    tile.PlaceUnit(UnitInfo.Unit);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Tile") {
            tile = collision.GetComponent<Tile>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Tile") {
            tile = null;
        }
    }
}
