using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFP : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform playerBody;
    float rotationX;
    float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        // playerBody.Rotate = Quaternion.Euler(0, rotationY, 0);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
