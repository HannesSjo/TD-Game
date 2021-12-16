using UnityEngine;

public class TriShot : Projectile {
    public GameObject sideProjectile;

    public Transform[] sideAim;
    protected override void Setup() {
        for (int i = 0; i < 2; i++) {
            GameObject inst = (GameObject)Instantiate(sideProjectile, transform);
            bool isCrit = false;
            if (Parent.CritChance > UnityEngine.Random.Range(0, 100))
                isCrit = true;

            inst.GetComponent<Projectile>().SetProjectile(Pierces, Speed, Damage, isCrit, ArmorPiercing, sideAim[i].position, Parent);

        }
    }
}
