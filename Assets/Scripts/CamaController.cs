using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaController : MonoBehaviour, IInteractuable
{
    CicloDiaYNoche ciclo;
    ItemData item;

    public void Interactuar()
    {
        ciclo.FundidoANegro();
        NecesidadController nc = GetComponent<NecesidadController>();

        foreach (Necesidades n in item.statsSuma)
        {
            nc.SetNecesidadPlayer(n);
        }

        foreach (Necesidades n in item.statsRestar)
        {
            n.valor = -(n.valor);
            print("-n.valor = " + -n.valor);
            nc.SetNecesidadPlayer(n);
        }
    }
}
