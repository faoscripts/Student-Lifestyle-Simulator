using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pc : MonoBehaviour, IInteractuable
{
    GameObject PCShop;
    void Start(){
        PCShop = FindObjectOfType<PCShop>().gameObject;
        PCShop.SetActive(false);
    }

    public void Interactuar()
    {
        PCShop.SetActive(true);
        OptionsMenu.ShowCursor();
    }

    public void Exit()
    {
        PCShop.SetActive(false);
        OptionsMenu.HideCursor();
    }
}
