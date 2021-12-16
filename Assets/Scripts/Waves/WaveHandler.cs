using UnityEngine;
public class WaveHandler : MonoBehaviour {
    public Wave[] Waves;
    public float WaveWaitTime = 5f;
    private float nextWaveTimer;
    private CoinHandler coinHandler;

    private bool activeWave = false;
    public bool ActiveWave {
        set {
            if (value == false) {
                nextWaveTimer = WaveWaitTime;
                wave = GetNextWave();
            }
            activeWave = value;
        }
    }
    private Wave wave;
    private int curWave;
    private void Start() {
        coinHandler = FindObjectOfType<CoinHandler>();
        nextWaveTimer = WaveWaitTime / 2;
        curWave = -1;
        wave = GetNextWave();
    }

    private void Update() {
        if (!activeWave)
            nextWaveTimer -= Time.deltaTime;
        if (nextWaveTimer <= 0) {
            activeWave = true;
            wave.SummonWave();
        }
    }
    private Wave GetNextWave() {
        if (curWave + 2 < Waves.Length) {
            curWave++;
        }
        Waves[curWave].Start(this);
        return Waves[curWave];
    }

    public void WaveCoinBunus(int amount) {
        coinHandler.GainCoins(amount);
    }
    public void EnemyDeath(Enemy enemy) {
        wave.EnemyDeath(enemy);
    }

    public Enemy Spawn(GameObject obj) {
        return Instantiate(obj).GetComponent<Enemy>(); ;
    }
}
