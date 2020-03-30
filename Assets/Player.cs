using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int fall = -20;

    [System.Serializable]
    public class PlayerStats {
        public int maxHealth = 100;
        private int _currentHealth;
        public int currentHealth {
            get {
                return _currentHealth;
            }
            set {
                _currentHealth = Mathf.Clamp(value,0,maxHealth);
            }
        }

        public void Init() {
            _currentHealth = maxHealth; 
        }
    }

    public PlayerStats stats = new PlayerStats();

    [SerializeField]
    private StatusBar statusBar;

    private void Start() {
        stats.Init();
        if (statusBar == null) {
            Debug.LogError("No Status Bar");
        } else {
            statusBar.SetHealth(stats.currentHealth, stats.maxHealth);
        }
    }
    
    private void Update() {
        if (transform.position.y <= fall) {
            DamagePlayer(9999999);
        }
    }

    public void DamagePlayer(int amount) {
        stats.currentHealth -= amount;
        if (stats.currentHealth <= 0) {
            // Debug.Log("KILL");

            GameMaster.KillPlayer(this);
        }

        statusBar.SetHealth(stats.currentHealth, stats.maxHealth);
    }

}
