using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;
    public Transform player;
    public Collider muroIzquierdo;
    public Collider muroDerecho;
    //necesito el ancho del collider del jugador para hacer el clamping
    private Collider playerCol;

    [Header("Movimiento")]
    public float speed = 8f;
    public float threshold = 0.05f;

    void Start()
    {
        //obtengo el collider del jugador para hacer el clamping (necesito su anchura)
        playerCol = player.GetComponent<Collider>();
    }

    void Update()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(cam.transform.position.z - player.position.z);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);

        Vector3 target = player.position;
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
        float distance = Mathf.Abs(player.position.x - target.x);

        //correcion de jittering cuando el raton esta muy cerca del player
        if (distance > threshold)
        {
            player.position = Vector3.MoveTowards(
                player.position,
                target,
                speed * Time.deltaTime
            );
        }
    }
}