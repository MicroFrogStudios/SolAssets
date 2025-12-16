using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject bulletBasic;
    private bool attacking = false;
    private bool attackSpread = false;
    private bool resting = false;

    private void Start()
    {
 
    }

    void Update()
    {

        if (!attacking && !resting)
        {
            attacking = true;
            switch (Random.Range(1, 2))
            {
                case 1:
                    Debug.Log("ataque 1");
                    attackSpread = true;
                    break;
                    /*case 2:
                        Debug.Log("");
                        break;*/
            }
            StartCoroutine(AttackCounter());
        }


        if (attackSpread)
        {
            randomSpread();
        }
        



    }


    void randomSpread()
    {
        if (Time.frameCount % 60 == 0)
        {
            float rx = Random.Range(-5, 5);
            float ry = Random.Range(-20, 0);
            FireBullet(rx, ry);
        }
    }

    IEnumerator AttackCounter()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started AttackCoroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished AttackCoroutine at timestamp : " + Time.time);
        attacking = false;
        attackSpread = false;
        resting = true;
        StartCoroutine(RestingCounter());
    }

    IEnumerator RestingCounter()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started RestingCoroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished RestingCoroutine at timestamp : " + Time.time);
        resting = false;
    }

    void FireBullet(float x, float y)
    {
       

        GameObject bullet = Instantiate(bulletBasic);
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletController>().direction = new Vector3(x, y, 0).normalized;



    }
}
