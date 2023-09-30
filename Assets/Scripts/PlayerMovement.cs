using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    Vector3 move;
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
        TxtI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.SimpleMove(move * speed);
        
        Action();
        if(Input.GetKeyDown(KeyCode.Mouse1) && itemSlot != null){
            IInteractuable interactuable = FindObjectOfType<Interactuar>().interactuableActual;
            if (interactuable == null)
            {
                Drop();
            }
        }

        HandAnimator();
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
                n.valor = n.valor < 0 ? n.valor : -n.valor;
                nc.SetNecesidadPlayer(n);
            }

            if (item.GetComponent<ItemController>().item.consumible) {
                Destroy(itemSlot);
                defaultHand.SetActive(true);
                if (TxtI.activeInHierarchy) TxtI.SetActive(false);
            }

            ItemData itemData = item.GetComponent<ItemController>().item;

            if (itemData.complex && itemData.relatedGO == null && itemData.resultGO) {
                Destroy(itemSlot);
                EquipItem(itemData.resultGO.GetComponent<ItemController>());
            }


        }
    }

    void Drop(){
        if(Input.GetKeyDown(KeyCode.Mouse1) && itemSlot != null){
            // if (swDrop) { swDrop = !swDrop; return; }
            GameObject item = itemSlot.transform.GetChild(0).gameObject;
            item.transform.parent = null;
            if(item.GetComponent<Rigidbody>()){
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.GetComponent<Rigidbody>().AddForce(transform.forward * throwStrength);
                item.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                int LayerDefault = LayerMask.NameToLayer("Default");
                item.layer = LayerDefault;
                foreach (Transform child in item.transform)
                {
                    child.gameObject.layer = LayerDefault;
                }
            }
            // Destroy(itemSlot.transform.GetChild(1).gameObject);
            Destroy(itemSlot.gameObject);
            defaultHand.SetActive(true);
            if (TxtI.activeInHierarchy) TxtI.SetActive(false);
            // itemSlot = null;
        }
    }

    public void DestroyEquipedItem(){
        Destroy(itemSlot);
        defaultHand.SetActive(true);
    }

    public void EquipItem(ItemController itemC){
        defaultHand.SetActive(false);
        const string HAND_CAMERA_NAME = "HandCameraHolder";
        GameObject handCamera = GameObject.Find(HAND_CAMERA_NAME);
        GameObject itemInstance = Instantiate(itemC.item.equipoPrefab, handCamera.transform.position, Quaternion.identity, handCamera.transform);
        // itemInstance.transform.parent = handCamera.transform;
        itemInstance.transform.GetChild(0).gameObject.AddComponent<Rigidbody>().isKinematic = true;
        itemInstance.transform.localPosition = itemC.item.equipoPrefab.transform.position;
        itemInstance.transform.localRotation = itemC.item.equipoPrefab.transform.rotation;
        int LayerHand = LayerMask.NameToLayer("Hand");
        itemInstance.transform.GetChild(0).gameObject.layer = LayerHand;
        foreach (Transform child in itemInstance.transform.GetChild(0).gameObject.transform)
        {
            child.gameObject.layer = LayerHand;
        }
        itemSlot = itemInstance;
    }

    void HandAnimator(){
        Animator anim = GetComponentInChildren<Animator>();
        if (move == Vector3.zero)
        {
            anim.SetFloat("Speed", 0, 1, Time.deltaTime);
        }else{
            anim.SetFloat("Speed", 1, 1, Time.deltaTime);
        }
    }
    
}
