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
            print("instantiate");
            GameObject itemInstance = Instantiate(item.equipoPrefab, handCamera.transform.position, Quaternion.identity, handCamera.transform);
            // itemInstance.transform.parent = handCamera.transform;
            itemInstance.transform.GetChild(0).gameObject.AddComponent<Rigidbody>().isKinematic = true;
            print("item.equipoPrefab.transform.position = " + item.equipoPrefab.transform.position);
            itemInstance.transform.localPosition = item.equipoPrefab.transform.position;
            itemInstance.transform.localRotation = item.equipoPrefab.transform.rotation;
            PlayerMovement.itemSlot = itemInstance;
            if (item.consumible) Destroy(gameObject);
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