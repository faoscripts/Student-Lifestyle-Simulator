using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Vector3 respawn;

    [Header("----------Camara----------")]
    //Traernos el contenedor de la camara
    public Transform cameraContainer;
    public float maxXMirar;
    public float minXMirar;
    private float camCurXRotation;
    public float sensibilidadCamara;
    public bool puedeMirar;

    [Header("----------Movimiento----------")]
    public float moveSpeed;

    private Vector2 inputActualMoviento;

    private Vector2 mouseDelta;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        puedeMirar = true;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Movimiento
        inputActualMoviento.x = Input.GetAxis("Horizontal");
        inputActualMoviento.y = Input.GetAxis("Vertical");

        //Camara
        mouseDelta.x = Input.GetAxis("Mouse X");
        mouseDelta.y = Input.GetAxis("Mouse Y");
    }

    private void FixedUpdate()
    {
        Move();
        //animacionCamara.SetFloat("speed", Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.z));
    }

    private void Move()
    {
        //Queremos que se mueve en base a su propio eje local
        Vector3 dir = transform.forward * inputActualMoviento.y + transform.right * inputActualMoviento.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;
        rigidbody.velocity = dir;
    }

    private void LateUpdate()
    {
        if (puedeMirar)
        {
            CamaraMirar();
        }
    }

    private void CamaraMirar()
    {
        //Rotar la camara en ambos ejes en base al
        // mouseDelta (Vector2) que nos devuelve Input System
        camCurXRotation += mouseDelta.y * sensibilidadCamara * Time.deltaTime;
        camCurXRotation = Mathf.Clamp(camCurXRotation, minXMirar, maxXMirar);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRotation, 0, 0);

        //Rotamos al jugador de izquierda a derecha
        transform.eulerAngles += new Vector3(0, mouseDelta.x * sensibilidadCamara * Time.deltaTime, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ActivarCursor(bool activar)
    {
        Cursor.lockState = activar ? CursorLockMode.None : CursorLockMode.Locked;
        puedeMirar = !activar;
    }
}// Cierre de la clase