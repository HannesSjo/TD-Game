using UnityEngine;

public class Horse : Enemy {
    public Enemy rider;

    public SpriteRenderer riderSprite;

    protected override void Start() {
        if (rider != null)
            riderSprite.sprite = rider.Sprite.sprite;
        else
            riderSprite.enabled = false;
        base.Start();
    }

    protected override void Death() {
        if (rider != null) {
            Enemy inst = Instantiate(rider);
            inst.MidWaySpawn(transform.position, curWaypoint, DistanceTravaled);
        }
        base.Death();
    }
}
