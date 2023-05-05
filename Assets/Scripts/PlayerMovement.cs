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
            print("enter Action item");
        }
    }

    void Drop(){
        if(Input.GetKeyDown(KeyCode.Mouse1) && itemSlot != null){
            if (swDrop) { swDrop = !swDrop; return; }
            // GameObject item = itemSlot.transform.GetChild(0).gameObject;
            itemSlot.transform.parent = null;
            itemSlot.GetComponentInChildren<Rigidbody>().isKinematic = false;
            itemSlot.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * throwStrength);
            Destroy(itemSlot.transform.GetChild(1).gameObject);
            defaultHand.SetActive(true);
            itemSlot = null;
        }
    }

    
}
