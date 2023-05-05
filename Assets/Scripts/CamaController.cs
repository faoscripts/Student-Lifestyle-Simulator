using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaController : MonoBehaviour, IInteractuable
{
    CicloDiaYNoche ciclo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interactuar()
    {
        ciclo.FundidoANegro();
    }
}
