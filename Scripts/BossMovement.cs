using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float smoothSpeed = 1f;
    private Vector3 centerPosition = new Vector3(0, 13.2600002f, 0);
    public Vector3 leftPosition = new Vector3(-7.26999998f, 13.2600002f, 0);
    public Vector3 rightPosition = new Vector3(9.89999962f, 13.2600002f, 0);
    public Vector3 DesiredPosition;
    public float MinimumDistance = 0.2f;

    private void Start()
    {
       SetDesiredPosition();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, DesiredPosition, smoothSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, DesiredPosition) < MinimumDistance)
        {
            SetDesiredPosition();
        }
    }

    private void SetDesiredPosition()
    {
        int r = Random.Range(0, 3);   
        switch(r)
        {
            case 0:
                DesiredPosition = leftPosition;
                break;
            case 1:
                DesiredPosition = centerPosition;
                break;
            case 2:
                DesiredPosition = rightPosition;
                break;
        }
        //Debug.Log("New Desired Position: " + DesiredPosition);
    }


}
