using UnityEngine;
using UnityEngine.Rendering;

public class BulletController : MonoBehaviour
{

    public float speed = 0.01f;
    public Vector3 direction = new Vector3(0,-1,0);
    
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    
    void Update()
    {

        transform.position += direction * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("floor"))
        {
            Destroy(gameObject);
        }
    }
}
