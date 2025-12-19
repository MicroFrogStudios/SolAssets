using Unity.VisualScripting;
using UnityEngine;

public class OrbBulletController : BulletController
{


    public float freq = 1, amp = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed;
        fireShader.GetFloat("_noiseVariation");
        fireShader.SetFloat("_noiseVariation", fireShader.GetFloat("_noiseVariation") + Time.deltaTime );

        transform.localScale = Vector3.one* (amp*Mathf.Sin(Time.time* freq) + 1.2f);
    }

}
