using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerStats playerStats = new PlayerStats();
    public int fall = -20;

    [System.Serializable]
    public class PlayerStats {
        public int Health = 100;
    }

    
    private void Update() {
        if (transform.position.y <= fall) {
            DamagePlayer(9999999);
        }
    }

    public void DamagePlayer(int amount) {
        playerStats.Health -= amount;
        if (playerStats.Health <= 0) {
            // Debug.Log("KILL");

            GameMaster.KillPlayer(this);
        }
    }

}
