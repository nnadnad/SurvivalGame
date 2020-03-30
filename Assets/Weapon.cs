using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public  int damage = 10;
    public LayerMask whatToHit;

    // for bullet
    public Transform FireBulletPrefab;
    public Transform FlashPrefab;
    public float effectSpawnRate = 10;
    private float timeToSpawn = 0;

    public Transform HitPrefabs;

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

        // if (Time.time >= timeToSpawn) {
        //     Effect();
        //     // StartCoroutine("Effect");
        //     timeToSpawn = Time.time + 1/effectSpawnRate;
        // }

        // Effect();

        // Debug.DrawLine (firePointPos, (mousePosition - firePointPos)*100, Color.black);
        if (hit.collider != null) {
            // Debug.DrawLine (firePointPos, hit.point, Color.red);
            // Debug.Log("Test");
            EnemyControl enemy = hit.collider.GetComponent<EnemyControl>();
            if (enemy != null) {
                enemy.DamageEnemy(damage);
                Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage.");
            }
        }

        if (Time.time >= timeToSpawn) {

            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null) {
                hitPos = (mousePosition - firePointPos)*50;
                hitNormal = new Vector3(9999,9999,9999);
            } else {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }


            Effect(hitPos, hitNormal);
            // StartCoroutine("Effect");
            timeToSpawn = Time.time + 1/effectSpawnRate;
        }



        
    }


    void Effect(Vector3 hitPos, Vector3 hitNormal) {
        Transform trail = (Transform) Instantiate (FireBulletPrefab, firePoint.position, firePoint.rotation);

        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null) {
            // SET POSITIONS

            lr.SetPosition (0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }
        
        Destroy(trail.gameObject, 0.05f);

        if (hitNormal != new Vector3 (9999, 9999, 9999)) {
            Transform hitParticle = (Transform) Instantiate(HitPrefabs, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(hitParticle.gameObject, 0.5f);
        }


        Transform flash = (Transform) Instantiate (FlashPrefab, firePoint.position, firePoint.rotation);

        flash.parent = firePoint;

        float size = Random.Range(0.5f, 1.0f);

        flash.localScale = new Vector3(size,size,size);
        // yield return 0;
        Destroy(flash.gameObject, 0.05f);
    }
}
