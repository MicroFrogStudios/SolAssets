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
    public ParticleSystem shieldIndicator;
    public float shieldCooldown = 6f;


    private float shieldReactivationTime = 0f;
    private GameObject shieldInstance;
    private AudioSource shotSound;
   
    private void Awake()
    {

        shotSound = GetComponents<AudioSource>()[1];
        if (shieldObject != null)
        {
            shieldInstance = Instantiate(shieldObject);
            shieldInstance.GetComponent<FollowPlayer>().targetToFollow = transform;
            shieldInstance.GetComponent<MeshRenderer>().enabled = false;
            shieldInstance.GetComponent<MeshCollider>().enabled = false;
        }
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && !shieldInstance.GetComponent<MeshRenderer>().enabled && !shieldInstance.GetComponent<MeshCollider>().enabled)
        {
            ShootProjectile();
            mesh.GetComponent<Animator>().SetTrigger("lanzar");
            nextFireTime = Time.time + shootCooldown;
        }

        ActivateShield();
    }

    private void ActivateShield()
    {

        if (shieldInstance != null && shieldReactivationTime < Time.time)
        {
            
            shieldIndicator.Play();

            if (Input.GetMouseButton(1))
            {
                shieldIndicator.Stop();
                shieldInstance.GetComponent<MeshRenderer>().enabled = true;
                shieldInstance.GetComponent<MeshCollider>().enabled = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                shieldInstance.GetComponent<MeshRenderer>().enabled = false;
                shieldInstance.GetComponent<MeshCollider>().enabled = false;
                shieldReactivationTime = Time.time + shieldCooldown;
                shieldIndicator.Stop();

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
        shotSound.Play();
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