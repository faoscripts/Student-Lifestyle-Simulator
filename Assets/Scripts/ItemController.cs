using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IInteractuable
{
    public ItemData item;

    void Start(){
        if(gameObject.GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().mass = item.itemWeight;
    }

    public void Interactuar()
    {
        if (item.grab && PlayerMovement.itemSlot == null) {
            GameObject.Find("Hand").SetActive(false);
            
            GameObject handCamera = GameObject.Find("HandCamera");
            GameObject itemInstance = Instantiate(item.equipoPrefab, handCamera.transform.position, Quaternion.identity, handCamera.transform);
            // itemInstance.transform.parent = handCamera.transform;
            itemInstance.transform.GetChild(0).gameObject.AddComponent<Rigidbody>().isKinematic = true;
            itemInstance.transform.localPosition = item.equipoPrefab.transform.position;
            itemInstance.transform.localRotation = item.equipoPrefab.transform.rotation;
            print("layer item name = " + itemInstance.name);
            int LayerHand = LayerMask.NameToLayer("Hand");
            itemInstance.transform.GetChild(0).gameObject.layer = LayerHand;
            PlayerMovement.itemSlot = itemInstance;
            Destroy(gameObject);
            PlayerMovement.swDrop = true;
        }
        //if (InventoryController.instance.imageSlot[i].GetComponent<Image>().sprite == null)
        //{
        //    EquipoManager.instancia.Equipar(item);
        //    Destroy(gameObject);
        //}
    }

    private void Update()
    {
        
    }
}