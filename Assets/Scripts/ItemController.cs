using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            GameObject.Find("Hand").SetActive(false);
            
            GameObject handCamera = GameObject.Find("HandCamera");
            GameObject itemInstance = Instantiate(item.equipoPrefab, handCamera.transform.position, Quaternion.identity, handCamera.transform);
            // itemInstance.transform.parent = handCamera.transform;
            itemInstance.transform.GetChild(0).gameObject.AddComponent<Rigidbody>().isKinematic = true;
            itemInstance.transform.localPosition = item.equipoPrefab.transform.position;
            itemInstance.transform.localRotation = item.equipoPrefab.transform.rotation;
            int LayerHand = LayerMask.NameToLayer("Hand");
            itemInstance.transform.GetChild(0).gameObject.layer = LayerHand;
            PlayerMovement.itemSlot = itemInstance;
            Destroy(gameObject);
            PlayerMovement.swDrop = true;

            PlayerMovement.TxtI.GetComponent<TMP_Text>().text = "Pulsa LMB para interactuar con el objeto equipado \n Pulsa RMB para soltar";
            if (!PlayerMovement.TxtI.activeInHierarchy && CicloDiaYNoche.contadorDias <= CicloDiaYNoche.daysTutorial) {
                PlayerMovement.TxtI.SetActive(true);
            }
        }else if(!item.grab){
            if(am != null)
            {
                if (status == false)
                {
                    am.Play(item.soundName);
                    status = true;
                }else{
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

    private void Update()
    {
        
    }
}