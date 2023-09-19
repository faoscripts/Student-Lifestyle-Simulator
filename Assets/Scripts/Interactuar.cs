using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    AudioManager am;
    // GameObject PlayerMovement.TxtI;
    string bkGO;
    // Start is called before the first frame update
    void Start()
    {
        puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(3, 3);
        am = FindObjectOfType<AudioManager>();
        // PlayerMovement.TxtI = puntero.GetComponentInChildren<TMP_Text>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        bool validation = false;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(interactuableActual != null)
            {
                interactuableActual.Interactuar();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, distanciaInteraccion, layerRaycast)
            && !PlayerMovement.itemSlot)
        {
            if (hit.collider.gameObject.name == bkGO || hit.collider.gameObject == null) return;
            // print("hit.collider.gameObject = " + hit.collider.gameObject);
            // print("bkGO1 = " + bkGO);
            // print("bkGO2 = " + bkGO);
            if (hit.collider.TryGetComponent<IInteractuable>(out IInteractuable iinteractuableRetraido))
            {
                bkGO = hit.collider.gameObject.name;
                // print("hit.collider.iinteractuableRetraido = " + iinteractuableRetraido);
                interactuableActual = iinteractuableRetraido;
                puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(6, 6);
                PlayerMovement.TxtI.GetComponent<TMP_Text>().text = "Pulsa RMB para interactuar";
                if (!PlayerMovement.TxtI.activeInHierarchy && CicloDiaYNoche.contadorDias <= CicloDiaYNoche.daysTutorial) PlayerMovement.TxtI.SetActive(true);
                validation = true;
            }
            // else
            // {
            //     print("exit");
            //     interactuableActual = null;
            //     puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(3, 3);
            //     if (PlayerMovement.TxtI.activeInHierarchy) PlayerMovement.TxtI.SetActive(false);
            // }
        }
        // else
        if (!validation)
        {
            if (bkGO == null) return;
            bkGO = null;
            interactuableActual = null;
            puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(3, 3);
            if (!PlayerMovement.itemSlot) PlayerMovement.TxtI.SetActive(false);
        }
    }
}

public interface IInteractuable
{
    public void Interactuar();
}
