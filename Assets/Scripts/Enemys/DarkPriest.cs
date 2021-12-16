using System.Collections.Generic;
using UnityEngine;

public class DarkPriest : Enemy {

    public float HealCd = 5;
    public float HealTime = 0.5f;
    public int HealAmount = 40;
    private float timer;
    private float healTimer;

    [HideInInspector]
    public List<Enemy> Targets;

    private Animator animator;

    private bool casting = false;

    protected override void Start() {
        animator = GetComponent<Animator>();
        Targets = new List<Enemy>();
        timer = HealCd;
        healTimer = HealTime;
        Targets.Add(this);
        base.Start();
    }

    protected override void Update() {
        CheckHeal();

        if (!casting) {
            Movement();
            Rotate();
            Track();
        }
        HandleDebuffs();
    }

    private void CheckHeal() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            animator.SetBool("Heal", true);
            casting = true;
            CastHeal();
        }
    }

    private void CastHeal() {
        healTimer -= Time.deltaTime;
        if (healTimer <= 0) {
            casting = false;
            animator.SetBool("Heal", false);
            foreach (Enemy target in Targets) {
                target.Heal(HealAmount);
            }
            healTimer = HealTime;
            timer = HealCd;
        }
    }
}
