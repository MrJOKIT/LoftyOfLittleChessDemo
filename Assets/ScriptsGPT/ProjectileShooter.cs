using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Tooltip("The projectile prefab to shoot.")]
    public GameObject projectilePrefab;
    [Tooltip("The speed at which the projectile is shot.")]
    public float projectileSpeed = 10f;
    [Tooltip("The area in which the player triggers the shooting.")]
    public Collider2D playerArea;
    [Tooltip("The radius of the area circle.")]
    public float areaRadius = 5f;
    [Tooltip("The color of the area circle.")]
    public Color areaColor = Color.red;

    private Transform playerTransform;
    private CircleCollider2D areaCollider;

    private void Start()
    {
        // Find the player transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Add a circle collider to the player area
        areaCollider = playerArea.gameObject.AddComponent<CircleCollider2D>();
        areaCollider.radius = areaRadius;
        areaCollider.isTrigger = true;

        // Add a line renderer to the player area to visualize the circle
        LineRenderer lineRenderer = playerArea.gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = areaColor;
        lineRenderer.positionCount = 32;

        // Set the positions of the line renderer to form a circle
        Vector3[] positions = new Vector3[32];
        for (int i = 0; i < 32; i++)
        {
            float angle = i * Mathf.PI * 2 / 32;
            positions[i] = new Vector3(Mathf.Sin(angle) * areaRadius, Mathf.Cos(angle) * areaRadius, 0);
        }
        lineRenderer.SetPositions(positions);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the area
        if (other.CompareTag("Player"))
        {
            // Shoot the projectile towards the player, but miss the first shot
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f))) * projectileSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exited the area
        if (other.CompareTag("Player"))
        {
            // Destroy all projectiles in the scene
            foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("Projectile"))
            {
                Destroy(projectile);
            }
        }
    }
}
