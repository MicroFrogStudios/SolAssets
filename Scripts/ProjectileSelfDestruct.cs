using UnityEngine;

public class ProjectileSelfDestruct : MonoBehaviour
{
    //la colision ignora al player
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }
        if (collision.gameObject.CompareTag("bullet"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }
        Destroy(gameObject);
    }
}
