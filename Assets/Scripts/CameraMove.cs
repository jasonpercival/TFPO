using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;

    private float yaw;
    private float pitch;

    public RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform camera = Camera.main.transform;

        CameraMovement();
        hit = new RaycastHit();
        Debug.DrawRay(camera.position, camera.rotation * Vector3.forward * 10000.0f, Color.red);

        ray = new Ray(camera.position, camera.rotation * Vector3.forward);

    }

    void CameraMovement()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
