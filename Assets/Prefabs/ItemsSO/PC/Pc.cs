using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pc : MonoBehaviour, IInteractuable
{
    GameObject PCShop;
    void Start(){
        PCShop = FindObjectOfType<OptionsMenu>().shopScreen;
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
