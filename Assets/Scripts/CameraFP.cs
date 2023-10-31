using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFP : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform playerBody;
    float rotationX;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        OptionsMenu optionsMenu = FindObjectOfType<OptionsMenu>();
        if (optionsMenu.pauseScreen.activeInHierarchy || optionsMenu.shopScreen.activeInHierarchy) return;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
