using System;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [Tooltip("The projectile prefab to be instantiated.")]
    public GameObject projectilePrefab;
    [Tooltip("The speed at which the projectile will be launched.")]
    public float launchSpeed = 10f;
    [Tooltip("The position where the projectile will be instantiated.")]
    public Transform spawnPosition;

    [SerializeField] private float throwCost;
    private PlayerMana playerMana;
    private void Start()
    {
        playerMana = GetComponent<PlayerMana>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerMana.Mp >= throwCost)
        {
            playerMana.Mp -= throwCost;
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
