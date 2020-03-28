using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    
    
    public static GameMaster gameMaster;
    public Transform playerPrefabs;
    public Transform spawnPlayerPoint;

    public int spawnDelay = 2;

    private void Start() {
        if (gameMaster == null) {
            gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public IEnumerator RespawnPlayer(){
        Debug.Log("AUDIO GOES HERE");
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefabs, spawnPlayerPoint.position, spawnPlayerPoint.rotation);
    }

    public static void KillPlayer (Player  player) {
        Destroy(player.gameObject);
        gameMaster.StartCoroutine(gameMaster.RespawnPlayer());
        
    }
}
