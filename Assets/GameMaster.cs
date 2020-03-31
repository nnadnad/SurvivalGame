﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    
    
    public static GameMaster gameMaster;
    public Transform playerPrefabs;
    public Transform spawnPlayerPoint;

    //for effect respawn
    public Transform spawnPrefabs;


    // public Transform enemyDeathParticles;
    public CameraShake cameraShake;

    // public int scoreValue = 10;

    public float spawnDelay = 2;

    private void Awake() {
        if (gameMaster == null) {
            gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public IEnumerator _RespawnPlayer(){
        GetComponent<AudioSource>().Play();
        // Debug.Log("AUDIO GOES HERE");
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefabs, spawnPlayerPoint.position, spawnPlayerPoint.rotation);
        Transform spawn = (Transform) Instantiate(spawnPrefabs, spawnPlayerPoint.position, spawnPlayerPoint.rotation);

        spawn.parent = spawnPlayerPoint;

        Destroy(spawn.gameObject, 2f);
        // Debug.Log("Spawn Particles for effect");
    }

    public static void KillPlayer (Player  player) {
        Destroy(player.gameObject);
        gameMaster.StartCoroutine(gameMaster._RespawnPlayer());
        
    }

    public static void KillEnemy(EnemyControl enemy) {
        // Score.scorePlayer +  gameMaster.scoreValue;
        gameMaster._KillEnemy(enemy);
    }

    public void _KillEnemy(EnemyControl _enemy) {

        Transform _clone = (Transform) Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
        Destroy(_clone.gameObject, 0.05f);
        cameraShake.shake(_enemy.shakeAmout, _enemy.shakeLength);

        Destroy(_enemy.gameObject);
    }
}
