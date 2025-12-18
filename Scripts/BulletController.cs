using UnityEngine;
using UnityEngine.Rendering;

public class BulletController : MonoBehaviour
{

    public Material fireShader;

    public float speed = 0.01f;
    public Vector3 direction = new Vector3(0,-1,0);
    
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    
    void Update()
    {

        transform.position += direction * speed;
        fireShader.GetFloat("_noiseVariation");
        fireShader.SetFloat("_noiseVariation", fireShader.GetFloat("_noiseVariation")+ Time.deltaTime*6);
        transform.Rotate(Vector3.Cross(direction,Vector3.up), 1.2f);
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("floor"))
        {
            Destroy(gameObject);
        }
    }
}
