using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour {

    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 0.4f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    public static void Create(Transform damagePopup, int damageAmount, Vector3 position, bool isCrit) {
        Transform dP = Instantiate(damagePopup, position, Quaternion.identity);
        dP.GetComponent<DamagePopup>().Setup(damageAmount, isCrit);
    }
    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
        disappearTimer = 0.2f;
    }
    public void Setup(int damageAmount, bool isCrit) {
        textMesh.SetText(damageAmount.ToString());
        if (isCrit) {
            textMesh.fontSize = 4;
            textColor = new Color(247,62,0);
        } else {
            textMesh.fontSize = 3;
            textColor = new Color(247,190,0);
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(0.7f,1) * 2f;
    }

    private void Update() {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 5f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5) {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            float disappearSpeed = 18f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
                Destroy(gameObject);
        }
    }
}
