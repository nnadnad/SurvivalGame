using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    public int scoreValue = 10;
	[System.Serializable]
	public class EnemyStats {
		// public int Health = 100;
        public int maxHealth = 100;
        private int _currentHealth;
        public int currentHealth {
            get { 
                return _currentHealth; 
            }
            set { 
                _currentHealth = Mathf.Clamp(value, 0, maxHealth); 
            }
        }
        public int damage = 20;

        // public float shakeAmout = 0.1f;
        // public float shakeLength = 0.1f;

        public void Init() {
            currentHealth = maxHealth;
        }
	}
	
	public EnemyStats stats = new EnemyStats();


    public Transform deathParticles;

    public float shakeAmout = 0.1f;
    public float shakeLength = 0.1f;


    [SerializeField]
    private StatusBar statusBar;

    private void Start() {
        
        stats.Init();
        if (statusBar != null) {
            statusBar.SetHealth(stats.currentHealth, stats.maxHealth);
        }

        if (deathParticles == null) {
            Debug.LogError("No death particle on enemy");
        }
    }
	
	public void DamageEnemy (int damage) {
		stats.currentHealth -= damage;
		if (stats.currentHealth <= 0)
		{
            Score.scorePlayer += scoreValue;
			GameMaster.KillEnemy (this);
		}

        if (statusBar != null) {
            statusBar.SetHealth(stats.currentHealth, stats.maxHealth);
        }
	}


    private void OnCollisionEnter2D(Collision2D colliderInfo) {
        Player player   = colliderInfo.collider.GetComponent<Player>();
        if (player != null) {
            player.DamagePlayer(stats.damage);
        }
    }
}
