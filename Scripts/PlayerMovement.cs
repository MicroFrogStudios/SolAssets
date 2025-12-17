using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;
    public Collider muroIzquierdo;
    public Collider muroDerecho;
    //necesito el ancho del collider del jugador para hacer el clamping
    private Collider playerCol;

    [Header("Movimiento")]
    public float speed = 8f;
    public float threshold = 0.05f;

    [Header("Stats")]
    public int liveMax = 3;
    private int lives;

    void Start()
    {
        //obtengo el collider del jugador para hacer el clamping (necesito su anchura)
        playerCol = GetComponent<Collider>();
        lives = liveMax;
    }

    void Update()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(cam.transform.position.z - transform.position.z);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);

        Vector3 target = transform.position;
        target.x = mouseWorld.x;

        //clamping (límites) para el jitter del jugador en las paredes

        //ancho del jugador
        float mitadJugador = playerCol.bounds.extents.x;

        //limite izq = borde der del muro izq + mitad del jugador
        float minX = muroIzquierdo.bounds.max.x + mitadJugador;

        //limite der = borde izq del muro der - mitad del jugador
        float maxX = muroDerecho.bounds.min.x - mitadJugador;

        //aplico las restricciones
        //mathf.Clamp obliga a target.x a estar entre el valor del minX y maxX
        target.x = Mathf.Clamp(target.x, minX, maxX);

        //movimiento dle player, esto ya no es del clamping
        float distance = Mathf.Abs(transform.position.x - target.x);

        //correcion de jittering cuando el raton esta muy cerca del player
        if (distance > threshold)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Debug.Log("life lost");
            lives--;
            Destroy(collision.gameObject);

            if (lives <= 0)
            {

                Destroy(gameObject);
            }
        }
    }
}