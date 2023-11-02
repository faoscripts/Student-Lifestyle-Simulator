using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractuable
{
    [SerializeField] Light[] lights;
    [SerializeField] AudioManager am;

    void Start(){
        am = FindObjectOfType<AudioManager>();
    }

    public void Interactuar()
    {
        am.Play("InterruptorLuz");
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled;
        }
    }
}
