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
    public IInteractuable interactuableActual;
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

        // if (PlayerMovement.itemSlot)
        // {
        //     GameObject item = PlayerMovement.itemSlot.transform.GetChild(0).gameObject;
        //     ItemData itemData = item.GetComponent<ItemController>().item;
        //     if (!itemData.complex) return;
        // }
        
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, distanciaInteraccion, layerRaycast))
        {
            if (hit.collider.gameObject.name == bkGO || hit.collider.gameObject == null) return;
            if (hit.collider.TryGetComponent<IInteractuable>(out IInteractuable iinteractuableRetraido))
            {
                if (PlayerMovement.itemSlot && hit.collider.gameObject.GetComponent<ItemController>()){
                    ItemData itemData = hit.collider.gameObject.GetComponent<ItemController>().item;
                    if (itemData.grab) return;
                }
                bkGO = hit.collider.gameObject.name;
                interactuableActual = iinteractuableRetraido;
                puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(6, 6);
                PlayerMovement.TxtI.GetComponent<TMP_Text>().text = "Pulsa clic derecho para interactuar";
                if (!PlayerMovement.TxtI.activeInHierarchy && CicloDiaYNoche.contadorDias <= CicloDiaYNoche.daysTutorial) PlayerMovement.TxtI.SetActive(true);
                validation = true;
            }
        }
        // else
        if (!validation)
        {
            if (bkGO == null) return;
            bkGO = null;
            interactuableActual = null;
            puntero.GetComponent<RectTransform>().sizeDelta = new Vector2(3, 3);
            PlayerMovement.TxtI.GetComponent<TMP_Text>().text = "Pulsa clic izquierdo para interactuar con el objeto equipado \n Pulsa clic derecho para soltar";
            // if (CicloDiaYNoche.contadorDias <= CicloDiaYNoche.daysTutorial) {
            //     PlayerMovement.TxtI.SetActive(true);
            // }
            if (!PlayerMovement.itemSlot) PlayerMovement.TxtI.SetActive(false);
        }
    }
}

public interface IInteractuable
{
    public void Interactuar();
}
