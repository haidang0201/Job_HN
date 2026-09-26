using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    public float distance = 5f;
    public float height = 2f;
    public float rotateSpeed = 150f;


    float yaw;


    void LateUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            yaw += Input.GetAxis("Mouse X") 
                   * rotateSpeed 
                   * Time.deltaTime;
        }


        Vector3 offset =
            Quaternion.Euler(0,yaw,0)
            * new Vector3(0,height,-distance);


        transform.position =
            target.position + offset;


        transform.LookAt(
            target.position + Vector3.up
        );
    }
}