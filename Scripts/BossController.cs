using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject bulletBasic;



    void Update()
    {
        if (Time.frameCount%100 == 0)
        {
            FireBullet();
        }

    }

    void FireBullet()
    {
        float rx = Random.Range(-10, 10);
        float ry = Random.Range(-20, 0);

        GameObject bullet = Instantiate(bulletBasic);
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletController>().direction = new Vector3(rx, ry, 0);



    }
}
