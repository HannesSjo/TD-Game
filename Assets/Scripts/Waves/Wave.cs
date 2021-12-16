using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Wave {
    public List<WaveEnemy> Enemys;
    private List<WaveEnemy> cpyEnemys;
    private WaveHandler waveHandler;
    public int WaveCoinBunus = 30;

    private List<Enemy> activeEnemys;
    private int count;
    public void Start(WaveHandler waveHandler) {
        this.waveHandler = waveHandler;
        cpyEnemys = new List<WaveEnemy>(Enemys);
        activeEnemys = new List<Enemy>();
    }

    private float timer = 0;
    public void SummonWave() {
        timer -= Time.deltaTime;
        if (timer <= 0 && cpyEnemys.Count > 0) {
            activeEnemys.Add(waveHandler.Spawn(cpyEnemys[0].Enemy));
            timer = cpyEnemys[0].TimeTillNext;
            if (cpyEnemys[0].Count <= 0) {
                cpyEnemys.RemoveAt(0);
            } else {
                cpyEnemys[0].Count--;
            }
        }
        CheckWaveStatus();
    }

    public void EnemyDeath(Enemy enemy) {
        activeEnemys.Remove(enemy);
    }

    private void CheckWaveStatus() {
        if (cpyEnemys.Count <= 0 && activeEnemys.Count <= 0) {
            waveHandler.ActiveWave = false;
            waveHandler.WaveCoinBunus(WaveCoinBunus);
        }
    }
}
