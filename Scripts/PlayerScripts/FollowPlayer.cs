using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Configuración de Seguimiento")]
    public Transform targetToFollow;
    public Vector3 offset;
    public bool useOffset = true;
    public float smoothSpeed = 0.125f;

    void Start()
    {
        if (useOffset && targetToFollow != null)
        {
            transform.position =  targetToFollow.position + offset;
        }
    }

    void LateUpdate()
    {
        if (targetToFollow != null)
        {
            Vector3 desiredPosition = targetToFollow.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}