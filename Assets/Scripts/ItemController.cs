using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IInteractuable
{
    public ItemData item;
    AudioManager am;
    bool status = false;
    // GameObject PlayerMovement.TxtI;

    void Start(){
        am = FindObjectOfType<AudioManager>();
        if(gameObject.GetComponent<Rigidbody>() == null && item.rb == true){
            gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().mass = item.itemWeight;
        }
        // PlayerMovement.TxtI = GameObject.FindWithTag(Tags.COMMANDS);
        if (item.spatialSound && !gameObject.GetComponent<AudioSource>()) am.setAudioSource(item.soundName,gameObject);
    }

    public void Interactuar()
    {
        if (item.grab && PlayerMovement.itemSlot == null) {
            FindObjectOfType<PlayerMovement>().EquipItem(this);
            Destroy(gameObject);
        }else if(PlayerMovement.itemSlot != null){
            ItemData equipItemData = PlayerMovement.itemSlot.transform.GetChild(0).GetComponent<ItemController>().item;
            if (!equipItemData.complex || equipItemData.relatedGO == null) return;
            GameObject playerItem = PlayerMovement.itemSlot.transform.GetChild(0).gameObject;
            ItemData itemData = playerItem.GetComponent<ItemController>().item;
            if (itemData.relatedGO.name == gameObject.name)
            {
                GameObject resultGO = itemData.resultGO;
                PlayerMovement PM = FindObjectOfType<PlayerMovement>();
                PM.DestroyEquipedItem();
                Instantiate(resultGO, new Vector3(transform.position.x, 
                    transform.position.y + 1, transform.position.z), Quaternion.identity);
            }
        }else if(!item.grab){
            if(am != null)
            {
                if (status == false)
                {
                    Necesidades[] statsSuma = item.statsSuma;
                    NecesidadController nc = FindObjectOfType<NecesidadController>();
                    
                    foreach(Necesidades n in statsSuma)
                    {
                        nc.SetNecesidadPlayer(n);
                    }

                    Necesidades[] statsResta = item.statsRestar;
                    
                    foreach(Necesidades n in statsResta)
                    {
                        n.valor = n.valor < 0 ? n.valor : -n.valor;
                        nc.SetNecesidadPlayer(n);
                    }
                    am.Play(item.soundName);
                    status = true;
                }else{ // wait end animation ?? how rn need 2 clicks
                    am.Pause(item.soundName);
                    status = false;
                }
            }
        }
        //if (InventoryController.instance.imageSlot[i].GetComponent<Image>().sprite == null)
        //{
        //    EquipoManager.instancia.Equipar(item);
        //    Destroy(gameObject);
        //}
    }
}