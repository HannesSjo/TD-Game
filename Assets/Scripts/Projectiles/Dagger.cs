using UnityEngine;

public class Dagger : Projectile {
    public GameObject dagger;
    public int Count = 1;

    public Vector2 Randomness;
    protected override void Setup() {
        for (int i = 0; i < Count; i++) {
            GameObject inst = (GameObject)Instantiate(dagger, transform);
            bool isCrit = false;
            if (Parent.CritChance > UnityEngine.Random.Range(0, 100))
                isCrit = true;

            Vector2 randomTarget = new Vector2(Target.x + Random.Range(Randomness.x * -1, Randomness.x), Target.y + Random.Range(Randomness.y * -1, Randomness.y));
            inst.GetComponent<Projectile>().SetProjectile(Pierces, Speed, Damage, isCrit, ArmorPiercing, randomTarget, Parent);
        }
    }
}
