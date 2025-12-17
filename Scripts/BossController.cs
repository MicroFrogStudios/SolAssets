using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("prefabs")]
    public GameObject bulletBasic;
    public GameObject bulletRay;

    [Header("sun parameters")]
    public int restingTime = 3;
    public int attackTime = 5;
    [Header ("bullet parameters")]
    public float spreadAngleLeft = 120f;
    public float spreadAngleRight = 240f;
    public float bulletSpeed = 0.02f;
    public float rateOfFire = 0.5f;


    private void Start()
    {
        StartCoroutine(RestingCoroutine());

        
    }

    void Update()
    {

    }

    /// <summary>
    /// Ataque basico del sol, escoge direcciones aleatorias y lanza una bala cada 60 frames que pasa en el juego.
    /// </summary>
    void RandomBullet()
    {

        float randomAngle = Random.Range(spreadAngleLeft, spreadAngleRight);
        
        float rx = Mathf.Sin(randomAngle * Mathf.Deg2Rad);
        float ry = Mathf.Cos(randomAngle * Mathf.Deg2Rad);

        FireBullet(rx, ry);

    }

    /// <summary>
    /// Corutina que define cuanto dura un ataque del boss
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started AttackCoroutine at timestamp : " + Time.time);
        //attacking = true;
        
        switch (Random.Range(1, 4))
        {
            case 1:
                Debug.Log("Balas Random");
                InvokeRepeating(nameof(RandomBullet), 0f, rateOfFire);
                break;
            case 2:
                Debug.Log("balas random rapido");
                InvokeRepeating(nameof(RandomBullet), 0f, rateOfFire*0.5f);
                break;
            case 3:
                Debug.Log("rayo");

                bulletRay.SetActive(true);
                break;
        }

        
        yield return new WaitForSeconds(attackTime);

        //After we have waited n seconds print the time again.
        Debug.Log("Finished AttackCoroutine at timestamp : " + Time.time);
        CancelInvoke();

        StartCoroutine(RestingCoroutine());
    }
    /// <summary>
    /// Corutina que define cuanto dura el periodo sin atacar del boss entre ataques.
    /// </summary>
    /// <returns></returns>
    IEnumerator RestingCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started RestingCoroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for n seconds.
        yield return new WaitForSeconds(restingTime);

        //After we have waited n seconds print the time again.
        Debug.Log("Finished RestingCoroutine at timestamp : " + Time.time);
        StartCoroutine(AttackCoroutine());
    }

    /// <summary>
    /// Dispara 1 bala basica en la direccion del vector (x,y)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void FireBullet(float x, float y)
    {
        GameObject bullet = Instantiate(bulletBasic);
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletController>().speed = bulletSpeed;
        bullet.GetComponent<BulletController>().direction = new Vector3(x, y, 0).normalized;
    }
}
