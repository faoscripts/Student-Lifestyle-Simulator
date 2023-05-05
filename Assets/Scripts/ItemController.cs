using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IInteractuable
{
    public ItemData item;

    public void Interactuar()
    {
        print("enter Itenractuar item controller");
        if (item.grab && PlayerMovement.itemSlot == null) {
            print("enter Itenractuar item controller 2");
            GameObject hand = GameObject.Find("Hand");
            GameObject itemInstance = Instantiate(item.equipoPrefab, hand.transform.position, Quaternion.identity);
            itemInstance.transform.parent = hand.transform;
            itemInstance.GetComponent<Rigidbody>().isKinematic = true;
            itemInstance.GetComponent<Rigidbody>().mass = item.itemWeight;
            PlayerMovement.itemSlot = itemInstance;
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