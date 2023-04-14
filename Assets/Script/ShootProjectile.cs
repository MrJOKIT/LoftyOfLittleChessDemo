using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [Tooltip("The projectile prefab to be instantiated.")]
    public GameObject projectilePrefab;
    [Tooltip("The speed at which the projectile will be launched.")]
    public float launchSpeed = 10f;
    [Tooltip("The position where the projectile will be instantiated.")]
    public Transform spawnPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition.position, transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * launchSpeed;
    }
}
