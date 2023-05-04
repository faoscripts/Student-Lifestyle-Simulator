using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IInteractuable
{
    public ItemData item;
    [SerializeField] float throwStrength;

    public void Interactuar()
    {
        if (item.grab && PlayerMovement.itemSlot == null) {
            GameObject hand = GameObject.Find("Hand");
            GameObject itemInstance = Instantiate(item.equipoPrefab, hand.transform.position, Quaternion.identity);
            itemInstance.transform.parent = hand.transform;
            itemInstance.GetComponent<Rigidbody>().isKinematic = true;
            PlayerMovement.itemSlot = itemInstance;
            print("enter intsntacne");
        }else{
            Drop();
        }
        //if (InventoryController.instance.imageSlot[i].GetComponent<Image>().sprite == null)
        //{
        //    EquipoManager.instancia.Equipar(item);
        //    Destroy(gameObject);
        //}
    }

    void Drop(){
        GameObject itemSlot = PlayerMovement.itemSlot;
        if(Input.GetKeyDown(KeyCode.Mouse1) && itemSlot != null){
            // GameObject item = itemSlot.transform.GetChild(0).gameObject;
            itemSlot.transform.parent = null;
            itemSlot.GetComponent<Rigidbody>().isKinematic = false;
            itemSlot.GetComponent<Rigidbody>().AddForce(transform.forward * throwStrength);
        }
    }

    private void Update()
    {
        
    }
}