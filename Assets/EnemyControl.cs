using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
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

        public void Init() {
            currentHealth = maxHealth;
        }

	}
	
	public EnemyStats stats = new EnemyStats();

    [SerializeField]
    private StatusBar statusBar;

    private void Start() {
        
        stats.Init();
        if (statusBar != null) {
            statusBar.SetHealth(stats.currentHealth, stats.maxHealth);
        }
    }
	
	public void DamageEnemy (int damage) {
		stats.currentHealth -= damage;
		if (stats.currentHealth <= 0)
		{
			GameMaster.KillEnemy (this);
		}

        if (statusBar != null) {
            statusBar.SetHealth(stats.currentHealth, stats.maxHealth);
        }



	}
}
