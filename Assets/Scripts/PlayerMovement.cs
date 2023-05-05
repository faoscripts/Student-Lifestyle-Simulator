using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public static GameObject itemSlot;
    [SerializeField] float throwStrength;
    public static bool swDrop = true;
    [SerializeField] GameObject defaultHand;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * speed * Time.deltaTime);
        
        Action();
        Drop();
    }

    void Action(){
        if(Input.GetKeyDown(KeyCode.Mouse0) && itemSlot != null){
            GameObject item = itemSlot.transform.GetChild(0).gameObject;
            item.GetComponent<AudioManager>().Play("Accion");
            Necesidades[] statsSuma = item.GetComponent<ItemController>().item.statsSuma;
            NecesidadController nc = GetComponent<NecesidadController>();
            
            foreach(Necesidades n in statsSuma)
            {
                nc.SetNecesidadPlayer(n);
            }

            Necesidades[] statsResta = item.GetComponent<ItemController>().item.statsRestar;
            
            foreach(Necesidades n in statsResta)
            {
                n.valor = -(n.valor);
                print("-n.valor = " + -n.valor);
                nc.SetNecesidadPlayer(n);
            }

            if (item.GetComponent<ItemController>().item.consumible) {
                Destroy(item);
                defaultHand.SetActive(true);
            }


        }
    }

    void Drop(){
        if(Input.GetKeyDown(KeyCode.Mouse1) && itemSlot != null){
            if (swDrop) { swDrop = !swDrop; return; }
            // itemSlot is Mano
            // item is Porro
            GameObject item = itemSlot.transform.GetChild(0).gameObject;
            item.transform.parent = null;
            if(item.GetComponent<Rigidbody>()){
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.GetComponent<Rigidbody>().AddForce(transform.forward * throwStrength);
                item.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                int LayerDefault = LayerMask.NameToLayer("Default");
                item.layer = LayerDefault;
            }
            // Destroy(itemSlot.transform.GetChild(1).gameObject);
            Destroy(itemSlot.gameObject);
            defaultHand.SetActive(true);
            // itemSlot = null;
        }
    }

    
}
