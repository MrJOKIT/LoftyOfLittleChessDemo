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
    private SoundManager soundManager;
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMana = GetComponent<PlayerMana>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && playerMana.Mp >= throwCost && playerMovement.CanMove) 
        {
            playerMana.Mp -= throwCost;
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        SoundManager.instace.Play(SoundManager.SoundName.ThrowSpear);
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition.position, transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * launchSpeed;
    }
}
