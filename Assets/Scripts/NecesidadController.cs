using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NecesidadController : MonoBehaviour
{
    [SerializeField]
    float salud;
    float multiplicadorSalud;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Necesidades[] necesidades;
    [SerializeField]
    Slider sliderSalud;
    [SerializeField]
    Slider[] slidersNecesidades;
    public static UnityEvent gameOverEv = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            anim.SetBool("abrirCerrar", !anim.GetBool("abrirCerrar"));
        }

        multiplicadorSalud = 0;
        for(int i = 0; i < necesidades.Length; i++)
        {
            if(necesidades[i].necesidadVital)
            {
                necesidades[i].AddNecesidad(-Time.deltaTime / 3);
                
                if(necesidades[i].valor <= 0)
                {
                    multiplicadorSalud++;
                }

                slidersNecesidades[i].value = necesidades[i].valor / 100;
            }
            else
            {
                necesidades[i].AddNecesidad(Time.deltaTime / 3);
                if(necesidades[i].valor >= 100)
                {
                    foreach(Necesidades n in necesidades)
                    {
                        if(n.nombre == "Higiene")
                        {
                            n.valor = n.valorMaximo;
                            break;
                        }
                    }
                    necesidades[i].valor = 0;
                }
                slidersNecesidades[i].value = necesidades[i].valor / 100;
            }
        }

        salud -= Time.deltaTime * multiplicadorSalud;
        sliderSalud.value = salud / 100;
        if (salud <= 0) gameOverEv.Invoke();
    }

    public void SetNecesidadPlayer(Necesidades necesidad){
        List<Necesidades> listNecesidades = new();
        listNecesidades.AddRange(necesidades);

        Necesidades nMatch = listNecesidades.Find(x => x.nombre == necesidad.nombre);
        nMatch.SetNecesidad(necesidad.valor, necesidad.multiplicadorVelocidad);
    }
}
