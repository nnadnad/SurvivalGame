using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public  float damage = 10;
    public LayerMask whatToHit;

    // for bullet
    public Transform FireBulletPrefab;
    public float effectSpawnRate = 10;
    private float timeToSpawn = 0;

    private float castFire = 0;
    Transform firePoint;

    // Start is called before the first frame update
    void Start() {
        firePoint = transform.Find("FireBullet");
        if (firePoint == null) {
            Debug.LogError("No FireBullet!");
        }
    }

    // Update is called once per frame
    void Update() {
        // Shoot();
        if (fireRate == 0) {
            if (Input.GetButtonDown ("Fire1")) {
                Shoot();
            }
        } else {
            if (Input.GetButton ("Fire1") && Time.time > castFire) {
                castFire = Time.time + 1 /fireRate;
                Shoot();
            }
        }
    }

    void Shoot() {
        // Debug.Log("Shoot");
        Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPos = new Vector2 (firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast (firePointPos, (mousePosition-firePointPos), 100, whatToHit);

        if (Time.time >= timeToSpawn) {
            Effect();
            timeToSpawn = Time.time + 1/effectSpawnRate;
        }

        // Effect();

        Debug.DrawLine (firePointPos, (mousePosition - firePointPos)*100, Color.black);
        if (hit.collider != null) {
            Debug.DrawLine (firePointPos, hit.point, Color.red);
            // Debug.Log("Test");
            Debug.Log("We hit " + hit.collider + " and did " + damage + " damage.");
        }
    }


    void Effect() {
        Instantiate (FireBulletPrefab, firePoint.position, firePoint.rotation);
    }
}
