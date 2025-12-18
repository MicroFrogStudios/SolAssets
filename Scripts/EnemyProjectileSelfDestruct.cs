using UnityEngine;

public class EnemyProjectileSelfDestruct : MonoBehaviour
{
    //la colision ignora al SolInvictus
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SolInvictus") || collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("bullet"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }
        Destroy(gameObject);
    }
}
