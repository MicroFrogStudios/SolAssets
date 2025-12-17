using UnityEngine;

public class ShootOnClick : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    public GameObject projectilePrefab;
    public float launchSpeed = 10f;
    public Transform launchPoint;
    public float projectileLifetime = 5f;
    public float shootCooldown = 0.5f;
    private float nextFireTime = 0f;
    public GameObject mesh;

    [Header("Configuración del Escudo")]
    public GameObject shieldObject;

    private void Start()
    {


        if (shieldObject != null)
        {
            shieldObject.GetComponent<MeshRenderer>().enabled = false;
            shieldObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && !shieldObject.GetComponent<MeshRenderer>().enabled && !shieldObject.GetComponent<MeshCollider>().enabled)
        {
            ShootProjectile();
            mesh.GetComponent<Animator>().SetTrigger("lanzar");
            nextFireTime = Time.time + shootCooldown;
        }

        ActivateShield();
    }

    private void ActivateShield()
    {
        if (shieldObject != null)
        {
            if (Input.GetMouseButton(1))
            {
                shieldObject.GetComponent<MeshRenderer>().enabled = true;
                shieldObject.GetComponent<MeshCollider>().enabled = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                shieldObject.GetComponent<MeshRenderer>().enabled = false;
                shieldObject.GetComponent<MeshCollider>().enabled = false;
            }
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