using UnityEngine;

public class RayController : MonoBehaviour
{


    public float sweepTime = 2;
    public int sweepsPerAttack = 8;
    public float sweepSpeed = 2f;

    public float startAngle = 180;
    public float sweepAngle = 30;


    
    private float raySweepRightTime= 0;
    private float raySweepLeftTime= 0;
    private int sweepsLeft = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.z < startAngle+sweepAngle)
        {
            transform.Rotate(transform.forward, -sweepSpeed);
            
            return;
        }
        if (transform.rotation.eulerAngles.z > startAngle - sweepAngle)
        {
            transform.Rotate(transform.forward, sweepSpeed);
            return;
        }

        if (sweepsLeft > 0)
        {
            sweepsLeft--;
            return;
        }

        GetComponentInParent<BossController>().finishedAttack = true;
        //terminar el ataque.
        
    }


    public void RaySweep()
    {
        raySweepRightTime = sweepTime;
        raySweepLeftTime = sweepTime;
        sweepsLeft = sweepsPerAttack;
    }
}
