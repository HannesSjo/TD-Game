using UnityEngine;
using UnityEngine.UI;

public class SpeedHandler : MonoBehaviour {
    public Image SpeedButton;
    public Sprite[] ButtonSprites;
    private bool speeded = false;
    public void ChangeSpeed() {
        if (!speeded) {
            Time.timeScale = 2;
            SpeedButton.sprite = ButtonSprites[1];
            speeded = true;
        }
        else {
            Time.timeScale = 1;
            SpeedButton.sprite = ButtonSprites[0];
            speeded = false;
        }
    }
}
