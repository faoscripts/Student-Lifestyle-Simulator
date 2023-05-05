using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecesidadController : MonoBehaviour
{
    [SerializeField]
    float salud;
    float multiplicadorSalud;

    [SerializeField]
    Necesidades[] necesidades;
    [SerializeField]
    

    // Update is called once per frame
    void Update()
    {
        multiplicadorSalud = 0;
        foreach(Necesidades n in necesidades)
        {
            if(n.necesidadVital)
            {
                n.AddNecesidad(-Time.deltaTime / 3, 0);
                if(n.valor <= n.valorMaximo)
                {
                    multiplicadorSalud++;
                }
            }
            else
            {
                n.AddNecesidad(Time.deltaTime / 3, 0);
                if(n.valor >= 100)
                {
                    foreach(Necesidades i in necesidades)
                    {
                        if(i.nombre == "Higiene")
                        {
                            i.valor = i.valorMaximo;
                        }
                    }
                    n.valor = 0;
                }
            }
            
        }
        salud -= Time.deltaTime * multiplicadorSalud;
    }
}
