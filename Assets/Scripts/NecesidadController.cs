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

    // Update is called once per frame
    void Update()
    {
        multiplicadorSalud = 0;
        foreach(Necesidades n in necesidades)
        {
            if(n.necesidadVital && n.valor <= 0)
            {
                multiplicadorSalud++;
            }
            n.AddNecesidad(-Time.deltaTime/3);
        }
        salud -= Time.deltaTime * multiplicadorSalud;
    }
}
