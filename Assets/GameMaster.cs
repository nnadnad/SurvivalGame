using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    
    
    public static GameMaster gameMaster;
    public Transform playerPrefabs;
    public Transform spawnPlayerPoint;

    //for effect respawn
    public Transform spawnPrefabs;

    public float spawnDelay = 2;

    private void Start() {
        if (gameMaster == null) {
            gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public IEnumerator RespawnPlayer(){
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
        gameMaster.StartCoroutine(gameMaster.RespawnPlayer());
        
    }

    public static void KillEnemy(EnemyControl enemy) {
        Destroy(enemy.gameObject);
    }
}
