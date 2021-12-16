using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
    public int MaxHealth = 100;
    protected int health;

    public int MaxDefence = 20;
    protected int defence;

    public float speed = 1f;
    public int Damage = 5;
    public int CoinDrop = 10;

    public float size = 1f;

    public float DistanceTravaled = 0f;

    public GameObject[] Waypoints;
    protected int curWaypoint;

    public HealthbarBehaviour Healthbar;

    public float DefenceReduction = 0f;
    private WaveHandler waveHandler;

    [SerializeField]
    private Transform damagePopup;

    private Vector2 oldPos;

    private List<Debuff> debuffs = new List<Debuff>();

    private float slow = 1f;

    public SpriteRenderer Sprite;
    private bool midWaySpawn = false;

    public int Tier = 1;

    public float Slow {
        set {
            slow += value;
        }
    }
    private bool stunned;
    public bool Stunned {
        get {
            return stunned;
        }
        set {
            stunned = value;
        }
    }

    private CoinHandler coinHandler;
    private HealthHandler healthHandler;
    protected virtual void Start() {
        coinHandler = FindObjectOfType<CoinHandler>();
        healthHandler = FindObjectOfType<HealthHandler>();
        waveHandler = FindObjectOfType<WaveHandler>();
        Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        if (!midWaySpawn) {
            transform.position = Waypoints[0].transform.position;
            curWaypoint = 1;
        }
        health = MaxHealth;
        defence = MaxDefence;
        Healthbar.SetHealth(health, MaxHealth);
        oldPos = transform.position;
    }

    protected virtual void Update() {
        if (!Stunned) {
            Movement();
            Rotate();
            Track();
        }
        HandleDebuffs();
    }

    protected void Track() {
        Vector2 distanceVector = (Vector2) transform.position - oldPos;
        float distanceThisFrame = distanceVector.magnitude;
        DistanceTravaled += distanceThisFrame;
        oldPos = transform.position;
    }

    protected void Rotate() {
        if (curWaypoint + 1 <= Waypoints.Length) {
            Vector2 dir = transform.position - Waypoints[curWaypoint].transform.position;
            dir.Normalize();
            
            if (dir.x > 0) {
                transform.localScale = new Vector2(size * -1, size);
            }
            else  if (dir.x < 0) {
                transform.localScale = new Vector2(size, size);
            }
        }
    }
    protected void Movement() {
        if (curWaypoint + 1 <= Waypoints.Length) {
            transform.position = Vector2.MoveTowards(transform.position, Waypoints[curWaypoint].transform.position, Time.deltaTime * (speed * slow));
            if (transform.position == Waypoints[curWaypoint].transform.position) {
                curWaypoint++;
            }
        }

        if(transform.position == Waypoints[Waypoints.Length - 1].transform.position) {
            AtEndOfRoad();
        }
    }

    protected void AtEndOfRoad() {

        healthHandler.Health = Damage * -1;

        waveHandler.EnemyDeath(this);
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int amount, bool armorPiercing, bool crit, DamageType damageType) {
        int totalAmount = amount;
        int totalDefence = Mathf.RoundToInt(defence-(defence*DefenceReduction));
        if (crit)
            totalAmount *= 2;
        if (!armorPiercing)
            totalAmount = Mathf.RoundToInt(totalAmount * (100f / (100f + defence)));

        health -= totalAmount;
        Healthbar.SetHealth(health, MaxHealth);
        DamagePopup.Create(damagePopup, totalAmount, transform.position, crit);

        if (health <= 0) {
            Death();
        }
    }

    public void Heal(int amount) {
        health = Mathf.Clamp(health + amount, 0, MaxHealth);
        Healthbar.SetHealth(health, MaxHealth);
    }

    public void AddDebuff(Debuff debuff) {
        if (health > 0 && debuff != null && gameObject != null) {
            if (!debuffs.Exists(x => x.GetType() == debuff.GetType())) {
                debuffs.Add(debuff);
                debuff.Start();
            }
        }

    }
    public void RemoveDebuff(Debuff debuff) {
            debuffs.Remove(debuff);
    }

    protected void HandleDebuffs() {
        Debuff[] debuffArray = debuffs.ToArray();

        for (int i = 0; i < debuffArray.Length; i++) {
            if (debuffArray[i] != null)
                debuffArray[i].Update();
        }
    }

    public void MidWaySpawn(Vector2 position, int wayPoint, float travelDistance) {
        midWaySpawn = true;
        transform.position = position;
        DistanceTravaled = travelDistance;
        curWaypoint = wayPoint;
    }

    protected virtual void Death() {
        waveHandler.EnemyDeath(this);
        coinHandler.GainCoins(CoinDrop);
        debuffs = null;
        Destroy(gameObject);
    }
}
