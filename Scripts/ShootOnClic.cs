using UnityEngine;

public class ShootOnClick : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    public GameObject projectilePrefab;
    public float launchSpeed = 10f;
    public Transform launchPoint;
    public float projectileLifetime = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("no hay prefab de proyectil");
            return;
        }

        Vector3 spawnPosition = launchPoint != null ? launchPoint.position : transform.position;

        GameObject newProjectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("no hay un Rigidbody, se añade uno automaticamente", newProjectile);
            rb = newProjectile.AddComponent<Rigidbody>();
        }

        rb.linearVelocity = Vector3.up * launchSpeed;

        Destroy(newProjectile, projectileLifetime);
    }
}