using UnityEngine;

public class Projectile : MonoBehaviour {
    protected int Pierces = 1;
    protected float Speed = 1f;
    protected int Damage = 50;
    protected bool Crit = true;
    protected bool ArmorPiercing = false;
    protected ProjectileUnit Parent;

    protected Vector3 Target;
    public Rigidbody2D Rb;

    public void SetProjectile(int pierces, float speed, int damage, bool crit, bool armorPiercing, Vector3 target, ProjectileUnit parent) {
        Pierces = pierces;
        Speed = speed;
        Damage = damage;
        ArmorPiercing = armorPiercing;
        Target = target;
        Parent = parent;
        Crit = crit;
    }

    protected virtual void Setup() { }
    void Start() {
        Vector3 targ = Target;

        Vector2 direction;

        if (Parent.transform.position.x > Target.x) 
            direction = transform.position - Target;
        else
            direction = Target - transform.position;

        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle));
   

        Vector2 dir = (this.transform.position - Target).normalized;
        Rb.velocity = dir * Speed * -1;
        Setup();
    }

    private void ApplyDebuff(Enemy target) {
        Debuff debuff = Parent.GetDebuff();
        if (debuff != null) {
            debuff.SetTarget(target);
            target.AddDebuff(debuff);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Enemy hit = collision.gameObject.GetComponent<Enemy>();
            hit.TakeDamage(Damage, ArmorPiercing, Crit, Parent.GetDamageType());
            ApplyDebuff(hit);
            Pierces--;
            if (Pierces <= 0) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == Parent.gameObject) {
            Destroy(gameObject);
        }
    }
}
