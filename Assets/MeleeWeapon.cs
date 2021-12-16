using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {
    public MeleeUnit Parent;
    private Animator animator;

    private bool attacking = false;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void Attack() {
        animator.SetTrigger("Attack");
        attacking = true;
    }

    public void AnimationEnd() {
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy" && attacking) {
            collision.GetComponent<Enemy>().TakeDamage(Parent.Damage,Parent.ArmorPiercing , false, Parent.GetDamageType());
        }
    }
}
