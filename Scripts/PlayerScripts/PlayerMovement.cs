using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referencias")]
    //public Camera cam;
    //public Collider muroIzquierdo;
    //public Collider muroDerecho;
    
    //necesito el ancho del collider del jugador para hacer el clamping
    private Collider playerCol;

    [Header("Movimiento")]
    public float speed = 8f;
    public float threshold = 0.05f;
    public Vector3 slideRotation;

    [Header("Límites de Pantalla")]
    public float xMinLimit = -13.5f;
    public float xMaxLimit = 13.5f;

    [Header("Stats")]
    public int liveMax = 3;
    private int lives;
    private AudioSource hurtSound;
    
    public event Action LostLife;

    void Start()
    {
        hurtSound = GetComponents<AudioSource>()[0];
        //obtengo el collider del jugador para hacer el clamping (necesito su anchura)
        playerCol = GetComponent<Collider>();
        lives = liveMax;
    }

    void Update()
    {

        //ratón a mundo
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);

        //target en X
        float targetX = mouseWorld.x;

        //clamping con los valores fijos
        //calculamos la mitad del ancho del jugador para que no "atraviese" el límite
        float mitadJugador = playerCol != null ? playerCol.bounds.extents.x : 0f;
        
        float minPermitido = xMinLimit + mitadJugador;
        float maxPermitido = xMaxLimit - mitadJugador;

        targetX = Mathf.Clamp(targetX, minPermitido, maxPermitido);

        //movimiento
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        float distance = Mathf.Abs(transform.position.x - targetX);

        //correcion de jittering cuando el raton esta muy cerca del player
        if (distance > threshold)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
            float orientation = targetPosition.x > transform.position.x ? 1f  : -1f;
            //girar el player hacia donde se mueve
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(orientation*slideRotation),0.6f);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 0.7f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Debug.Log("life lost");
            LostLife?.Invoke();
            lives--;
            hurtSound.Play();
            Destroy(collision.gameObject);

            if (lives <= 0)
            {

                Destroy(gameObject);
            }
        }
    }
}