using UnityEngine;

public class EnvironmentRotate : MonoBehaviour
{

    public float AngularSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(AngularSpeed, 0, 0,Space.World);
    }
}
