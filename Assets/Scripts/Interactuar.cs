using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactuar : MonoBehaviour
{
    Transform cameraTransform => Camera.main.transform;
    [SerializeField]
    float distanciaInteraccion;
    [SerializeField]
    LayerMask layerRaycast;
    [SerializeField]
    GameObject puntero;
    IInteractuable interactuableActual;

    // Start is called before the first frame update
    void Start()
    {
        puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(interactuableActual != null)
            {
                interactuableActual.Interactuar();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, distanciaInteraccion, layerRaycast))
        {
            if (hit.collider.TryGetComponent<IInteractuable>(out IInteractuable iinteractuableRetraido))
            {
                interactuableActual = iinteractuableRetraido;
                puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(6, 6);
            }
            else
            {
                interactuableActual = null;
                puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(3, 3);
            }
        }
        else
        {
            interactuableActual = null;
            puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(3, 3);
        }
    }
}

public interface IInteractuable
{
    public void Interactuar();
}
