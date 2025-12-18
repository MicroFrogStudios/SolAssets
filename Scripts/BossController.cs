using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("prefabs")]
    public GameObject bulletBasic;
    public GameObject bulletRay;

    [Header("sun parameters")]
    public float restingTime = 0.5f;
    public float attackTime = 5f;
    [Header ("bullet parameters")]
    public float defaultSpreadAngleLeft = 120f;
    public float defaultSpreadAngleRight = 240f;
    public float defaultBulletSpeed = 0.02f;
    public float rateOfFire = 0.5f;
    public Material SolMaterial;
    public int BossLiveMax = 50;
    public int BossLive;


    private float spreadAngleLeft;
    private float spreadAngleRight;
    private float bulletSpeed;

    public bool finishedAttack = false;
    private void Start()
    {
        
        BossLive = BossLiveMax;
        spreadAngleLeft = defaultSpreadAngleLeft;
        spreadAngleRight = defaultSpreadAngleRight;
        bulletSpeed = defaultBulletSpeed;

        StartCoroutine(RestingCoroutine());

    }

    void Update()
    {
        SolMaterial.GetFloat("_noiseVariation");
        SolMaterial.SetFloat("_noiseVariation", SolMaterial.GetFloat("_noiseVariation") + Time.deltaTime * 2);
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
        
        switch (Random.Range(1, 6))
        {
            case 1:
                Debug.Log("Balas Random");
                InvokeRepeating(nameof(RandomBullet), 0f, rateOfFire);
                break;
            case 2:
                Debug.Log("balas random rapido izquierda");
                spreadAngleLeft = defaultSpreadAngleLeft + 10;
                spreadAngleRight = defaultSpreadAngleRight - 90;
                bulletSpeed = defaultBulletSpeed * 2f;
                InvokeRepeating(nameof(RandomBullet), 0f, rateOfFire*0.5f);
                break;
            case 3:
                Debug.Log("balas random rapido en el centro");
                spreadAngleLeft = defaultSpreadAngleLeft + 50;
                spreadAngleRight = defaultSpreadAngleRight - 10;
                bulletSpeed = defaultBulletSpeed * 2f;
                InvokeRepeating(nameof(RandomBullet), 0f, rateOfFire * 0.5f);
                break;
            case 4:
                Debug.Log("balas random rapido derecha");
                spreadAngleLeft = defaultSpreadAngleLeft + 90;
                spreadAngleRight = defaultSpreadAngleRight - 50;
                bulletSpeed = defaultBulletSpeed * 2f;
                InvokeRepeating(nameof(RandomBullet), 0f, rateOfFire * 0.5f);
                break;
            case 5:
                Debug.Log("muchas balas lentas");
                bulletSpeed = defaultBulletSpeed * 0.6f;
                InvokeRepeating(nameof(RandomBullet), 0f, rateOfFire * 0.2f);
                break;
        }

        
        yield return new WaitForSeconds(attackTime);
        //yield return new WaitUntil(() => finishedAttack == true);
        finishedAttack = false;
        //After we have waited n seconds print the time again.
        Debug.Log("Finished AttackCoroutine at timestamp : " + Time.time);
        CancelInvoke();
        bulletRay.SetActive(false);
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
        spreadAngleLeft = defaultSpreadAngleLeft;
        spreadAngleRight = defaultSpreadAngleRight;
        bulletSpeed = defaultBulletSpeed;

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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("BossLive -1");
            BossLive--;
            Destroy(collision.gameObject);

            if (BossLive <= 0)
            {

                Destroy(gameObject);
            }
        }
    }

}
