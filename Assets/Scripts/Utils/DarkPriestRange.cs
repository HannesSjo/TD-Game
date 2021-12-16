using UnityEngine;

public class DarkPriestRange : MonoBehaviour {
    private DarkPriest parent;
    private void Start() {
        parent = GetComponentInParent<DarkPriest>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy")
            parent.Targets.Add(collision.GetComponent<Enemy>());
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Enemy")
            parent.Targets.Remove(collision.GetComponent<Enemy>());
    }

}
