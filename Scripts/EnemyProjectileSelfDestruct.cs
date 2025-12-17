using UnityEngine;

public class EnemyProjectileSelfDestruct : MonoBehaviour
{
    //la colision ignora al SolInvictus
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SolInvictus"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }
        Destroy(gameObject);
    }
}
