using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BossController : MonoBehaviour
{
    [Header("prefabs")]
    public GameObject bulletBasic;
    public GameObject bulletOrb;

    [Header("sun parameters")]
    public float restingTime = 0.5f;
    public float attackTime = 5f;
    public Material SolMaterial;
    public int BossLiveMax = 50;
    private int BossLive;
    [Header ("bullet parameters")]
    public const float defaultSpreadAngleLeft = 120f;
    public const float defaultSpreadAngleRight = 240f;
    public const float defaultBulletSpeed = 0.02f;
    public const float defaultBulletAcceleration = 1.2f;
    public const float rateOfFire = 0.5f;
    private float spreadAngleLeft;
    private float spreadAngleRight;
    private float bulletSpeed;
    private float bulletAcceleration = 1.2f;

    public bool finishedAttack = false;

    private GameObject player;
    private AudioSource fireSound;
    private void Start()
    {

        fireSound = GetComponent<AudioSource>();

        BossLive = BossLiveMax;
        spreadAngleLeft = defaultSpreadAngleLeft;
        spreadAngleRight = defaultSpreadAngleRight;
        bulletSpeed = defaultBulletSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
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

        FireBullet(rx, ry, bulletBasic);

    }

    void RandomOrb()
    {
        float randomAngle = Random.Range(spreadAngleLeft, spreadAngleRight);

        float rx = Mathf.Sin(randomAngle * Mathf.Deg2Rad);
        float ry = Mathf.Cos(randomAngle * Mathf.Deg2Rad);

        FireBullet(rx, ry, bulletOrb);
    }

    void AngleBullets()
    {
        float rx = Mathf.Sin(spreadAngleLeft * Mathf.Deg2Rad);
        float ry = Mathf.Cos(spreadAngleLeft * Mathf.Deg2Rad);

        FireBullet(rx, ry, bulletBasic);

        rx = Mathf.Sin(spreadAngleRight * Mathf.Deg2Rad);
        ry = Mathf.Cos(spreadAngleRight * Mathf.Deg2Rad);

        FireBullet(rx, ry, bulletBasic);
    }

    void FocusingBullets()
    {

        AngleBullets();
        spreadAngleLeft += 10;
        spreadAngleRight -= 10;

        RandomBullet();
        RandomBullet();
    }

    void LingeringBullets()
    {
        bulletSpeed *= bulletAcceleration;
        RandomOrb();
    }

    void TargetedBullet()
    {
        if (player != null)
        {
            Vector3 dirToPlayer = player.transform.position - transform.position;
            FireBullet(dirToPlayer.x, dirToPlayer.y, bulletBasic);
        }
    }

    void TargetedOrb()
    {
        if (player != null)
        {
            Vector3 dirToPlayer = player.transform.position - transform.position;
            FireBullet(dirToPlayer.x, dirToPlayer.y, bulletOrb);
        }
    }

    void BulletRaySweep()
    {

        

        spreadAngleLeft += 15;
        spreadAngleRight += 15;
        RandomBullet();
        RandomBullet();
        
    }

    void BulletAttack(float rateOfFire,float speed = defaultBulletSpeed, float angleLeft = defaultSpreadAngleLeft,float angleRight = defaultSpreadAngleRight,bool random = false)
    {
        spreadAngleLeft = angleLeft;
        spreadAngleRight = angleRight;
        bulletSpeed = speed;

        if (random)
        {
            InvokeRepeating(nameof(RandomBullet), 0f, rateOfFire);
        }
        else
        {
            InvokeRepeating(nameof(TargetedBullet), 0f, rateOfFire);
        }
        
    }

    void OrbAttack(float rateOfFire, float speed = defaultBulletSpeed, float angleLeft = defaultSpreadAngleLeft, float angleRight = defaultSpreadAngleRight, bool random = false)
    {
        spreadAngleLeft = angleLeft;
        spreadAngleRight = angleRight;
        bulletSpeed = speed;

        if (random)
        {
            InvokeRepeating(nameof(RandomOrb), 0f, rateOfFire);
        }
        else
        {
            InvokeRepeating(nameof(TargetedOrb), 0f, rateOfFire);
        }
    }

    /// <summary>
    /// Corutina que define cuanto dura un ataque del boss
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackCoroutine()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started AttackCoroutine at timestamp : " + Time.time);
        //attacking = true;
        int attackSelect = Random.Range(1, 9);
        switch (attackSelect)
        {
            case 1:
                //Debug.Log("Balas al jugador");
                BulletAttack( rateOfFire, random: true);
                
                break;
            case 2:
                //Debug.Log("balas random rapido izquierda");
                BulletAttack(rateOfFire*0.5f,angleLeft: defaultSpreadAngleLeft + 50,angleRight: defaultSpreadAngleRight - 90,speed: defaultBulletSpeed * 2f, random: true);
                break;
            case 3:
                //Debug.Log("balas random rapido en el centro");
                BulletAttack(rateOfFire * 0.5f, angleLeft: defaultSpreadAngleLeft + 55, angleRight: defaultSpreadAngleRight - 55, speed: defaultBulletSpeed * 2f, random: true);
                break;
            case 4:
                //Debug.Log("balas random rapido derecha");
                BulletAttack(rateOfFire * 0.5f, angleLeft: defaultSpreadAngleLeft + 90, angleRight: defaultSpreadAngleRight - 50, speed: defaultBulletSpeed * 2f, random: true);
                break;
            case 5:
                //Debug.Log("orbes random");
                OrbAttack(rateOfFire * 3f, speed: defaultBulletSpeed * 0.6f,random: true);
                break;
            case 6:
                //Debug.Log("balas convergentes");
                InvokeRepeating(nameof(FocusingBullets), 0f, rateOfFire*1.2f);
                break;
            case 7:
                //Debug.Log("Orbes que atacan");
                OrbAttack(rateOfFire*2, speed: defaultBulletSpeed * 1.2f);
                break;
            case 8:
                //Debug.Log("Barrido");
                spreadAngleRight = defaultSpreadAngleLeft + 5;
                InvokeRepeating(nameof(BulletRaySweep), 0f, rateOfFire*0.3f);
                break;

        }

        
        yield return new WaitForSeconds(attackTime);
        //yield return new WaitUntil(() => finishedAttack == true);
        finishedAttack = false;
        //After we have waited n seconds print the time again.
        //Debug.Log("Finished AttackCoroutine at timestamp : " + Time.time);
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
        //Debug.Log("Started RestingCoroutine at timestamp : " + Time.time);
        spreadAngleLeft = defaultSpreadAngleLeft;
        spreadAngleRight = defaultSpreadAngleRight;
        bulletSpeed = defaultBulletSpeed;
        bulletAcceleration = defaultBulletAcceleration;
        //yield on a new YieldInstruction that waits for n seconds.
        yield return new WaitForSeconds(restingTime);

        //After we have waited n seconds print the time again.
        //Debug.Log("Finished RestingCoroutine at timestamp : " + Time.time);
        StartCoroutine(AttackCoroutine());
    }

    /// <summary>
    /// Dispara 1 bala basica en la direccion del vector (x,y)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void FireBullet(float x, float y,GameObject bulletPrefab)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletController>().speed = bulletSpeed;
        bullet.GetComponent<BulletController>().direction = new Vector3(x, y, 0).normalized;
        fireSound.Play();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            //Debug.Log("BossLive -1");
            BossLive--;
            Destroy(collision.gameObject);

            if (BossLive <= 0)
            {

                Destroy(gameObject);
            }
        }
    }

}
