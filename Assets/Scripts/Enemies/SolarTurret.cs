using UnityEngine;

public class SolarTurret : Enemy {

    [Header("SolarTurret.cs")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float radius;
    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform rotationPoint;
    [SerializeField] float timer;
    float time;
    bool canShoot;

    public override void Awake() {
        base.Awake();
    }

    public override void Update() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject.CompareTag("Player")) {
                canShoot = true;
                break;
            }
        }

        if (canShoot) {
            time += Time.deltaTime;
            if (time >= timer) {
                Shoot();
                time = 0;
            }
        }

        canShoot = false;
    }

    private void Shoot() {
        Vector2 direction = player.position - transform.position;
        rotationPoint.right = direction;

        Rigidbody2D rb = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        Destroy(rb.gameObject, 2.5f);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}