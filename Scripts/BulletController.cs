using UnityEngine;
using UnityEngine.Rendering;

public class BulletController : MonoBehaviour
{

    public float speed = 5.0f;
    public Vector3 direction = new Vector3(0,-1,0);
    
    void Start()
    {

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
