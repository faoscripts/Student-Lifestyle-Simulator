using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NecesidadController : MonoBehaviour
{
    [SerializeField]
    float salud;
    float multiplicadorSalud;

    [SerializeField]
    Necesidades[] necesidades;
    [SerializeField]
    Slider[] slidersNecesidadesVitales;
    [SerializeField]
    Slider[] slidersNecesidadesSecundarias;

    // Update is called once per frame
    void Update()
    {
        multiplicadorSalud = 0;
        foreach(Necesidades n in necesidades)
        {
            if(n.necesidadVital)
            {
                n.AddNecesidad(-Time.deltaTime / 3);
                
                if(n.valor <= 0)
                {
                    multiplicadorSalud++;
                }

            }
            else
            {
                n.AddNecesidad(Time.deltaTime / 3);
                if(n.valor >= 100)
                {
                    foreach(Necesidades i in necesidades)
                    {
                        if(i.nombre == "Higiene")
                        {
                            i.valor = i.valorMaximo;
                            break;
                        }
                    }
                    n.valor = 0;
                }

            }
            
        }
        salud -= Time.deltaTime * multiplicadorSalud;
    }

    public void SetNecesidadPlayer(Necesidades necesidad){
        List<Necesidades> listNecesidades = new();
        listNecesidades.AddRange(necesidades);

        Necesidades nMatch = listNecesidades.Find(x => x.nombre == necesidad.nombre);
        nMatch.SetNecesidad(necesidad.valor, necesidad.multiplicadorVelocidad);
    }
}
