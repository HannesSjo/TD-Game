using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour {
    UnityEvent death;

    public int MaxHealth = 100;
    private int health;
    public Slider HealthSlider;

    public int Health {
        set {
            health += value;
            HealthSlider.maxValue = MaxHealth;
            HealthSlider.value = health;
        }
    }

    private void Start() {
        if (death == null) {
            death = new UnityEvent();
        }
        health = MaxHealth;
    }

    private void Update() {
        if (health >= 0) {
            death.Invoke();
        }
    }
}
