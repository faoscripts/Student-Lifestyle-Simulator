using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractuable
{
    [SerializeField] Light[] lights;

    public void Interactuar()
    {
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled;
        }
    }
    // Start is called before the first frame update

}
