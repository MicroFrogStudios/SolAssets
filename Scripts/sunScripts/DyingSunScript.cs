using System.Net.NetworkInformation;
using UnityEngine;

public class DyingSunScript : MonoBehaviour
{

    public GameObject body, crown;
    public ParticleSystem explosion;
    public Light dirLight, PointLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        body.GetComponent<Animator>().SetTrigger("muerte");
        crown.GetComponent<Animator>().SetTrigger("muerte");
        
        explosion.Play();
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        dirLight.intensity -= Mathf.MoveTowards(dirLight.intensity, 5, Time.deltaTime*0.1f);
        PointLight.intensity =Mathf.Lerp(PointLight.intensity,50,Time.deltaTime * 0.1f);
        PointLight.colorTemperature = Mathf.Lerp(PointLight.colorTemperature, 16854, Time.deltaTime * 0.1f);
    }
}
