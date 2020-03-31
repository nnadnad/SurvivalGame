using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    
    
    public static GameMaster gameMaster;
    // private static int _remainingLives = 3;
    // public static int RemainingLives {
    //     get {return _remainingLives;}
    // }
    public Transform playerPrefabs;
    public Transform spawnPlayerPoint;
    public Transform spawnEnemyPoint;

    //for effect respawn
    public Transform spawnPrefabs;
    public string spawnSoundName;
    // public Transform enemyPrefabs;


    public Transform enemyDeathParticles;
    public CameraShake cameraShake;

    [SerializeField]
    private GameObject gameOverUI;
    private AudioManager audioManager;

    // public int scoreValue = 10;

    public float spawnDelay = 2;

    private void Awake() {
        if (gameMaster == null) {
            gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    private void Start() {
        if (cameraShake == null) {
            Debug.LogError("no camera shake");
        }

        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found");
        }
    }

    public void EndGame(){
        gameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer(){
        // GetComponent<AudioSource>().Play();
        audioManager.PlaySound(spawnSoundName);
        // Debug.Log("AUDIO GOES HERE");
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefabs, spawnPlayerPoint.position, spawnPlayerPoint.rotation);
        Transform spawn = (Transform) Instantiate(spawnPrefabs, spawnPlayerPoint.position, spawnPlayerPoint.rotation);

        // spawn.parent = spawnPlayerPoint;

        Destroy(spawn.gameObject, 2f);
        // Debug.Log("Spawn Particles for effect");
    }

    public static void KillPlayer (Player  player) {
        // player.transform.position = player.
        Destroy(player.gameObject);
        // _remainingLives--;
        gameMaster.EndGame();
        // gameMaster.StartCoroutine(gameMaster._RespawnPlayer());
        
    }

    public static void KillEnemy(EnemyControl enemy) {
        // Score.scorePlayer +  gameMaster.scoreValue;
        gameMaster._KillEnemy(enemy);
    }

    public void _KillEnemy(EnemyControl _enemy) {

        Transform _clone = (Transform) Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
        Destroy(_clone.gameObject, 0.05f);
        cameraShake.shake(_enemy.shakeAmout, _enemy.shakeLength);

        // Instantiate(enemyPrefabs, spawnEnemyPoint.position, spawnEnemyPoint.rotation);
        // Transform enemySpawn = (Transform) Instantiate(enemyPrefabs, spawnEnemyPoint.position, spawnEnemyPoint.rotation);

        // Audio audio = GetComponent<AudioSource>().Play;
        Destroy(_enemy.gameObject);
    }
}
