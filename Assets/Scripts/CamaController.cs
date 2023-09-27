using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaController : MonoBehaviour, IInteractuable
{
    CicloDiaYNoche ciclo;
    [SerializeField] ItemData item;

    public void Interactuar()
    {
        ciclo = GameObject.FindObjectOfType<CicloDiaYNoche>();
        ciclo.Sleep();
        NecesidadController nc = FindObjectOfType<NecesidadController>();

        foreach (Necesidades n in item.statsSuma)
        {
            nc.SetNecesidadPlayer(n);
        }

        foreach (Necesidades n in item.statsRestar)
        {
            n.valor = n.valor < 0 ? n.valor : -n.valor;
            nc.SetNecesidadPlayer(n);
        }
    }
}
