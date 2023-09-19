using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public static GameObject itemSlot;
    public static GameObject TxtI;
    [SerializeField] float throwStrength;
    public static bool swDrop = true;
    [SerializeField] GameObject defaultHand;
    [SerializeField] AudioManager am;
    // GameObject PlayerMovement;

    void Start(){
        TxtI = GameObject.FindWithTag(Tags.COMMANDS);
        PlayerMovement.TxtI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.SimpleMove(move * speed);
        
        Action();
        Drop();
    }

    void Action(){
        if(Input.GetKeyDown(KeyCode.Mouse0) && itemSlot != null){
            GameObject item = itemSlot.transform.GetChild(0).gameObject;
            if(am != null)
            {
                am.Play(item.GetComponent<ItemController>().item.soundName);
            }
            
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
                nc.SetNecesidadPlayer(n);
            }

            if (item.GetComponent<ItemController>().item.consumible) {
                Destroy(itemSlot);
                defaultHand.SetActive(true);
                if (TxtI.activeInHierarchy) TxtI.SetActive(false);
            }


        }
    }

    void Drop(){
        if(Input.GetKeyDown(KeyCode.Mouse1) && itemSlot != null){
            if (swDrop) { swDrop = !swDrop; return; }
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
            if (TxtI.activeInHierarchy) TxtI.SetActive(false);
            // itemSlot = null;
        }
    }

    
}
