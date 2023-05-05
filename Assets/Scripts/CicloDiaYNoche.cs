using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CicloDiaYNoche : MonoBehaviour
{
    bool dia;
    [SerializeField]
    float velocidad;
    [SerializeField]
    GameObject sol;
    float exposure;

    [Header("-----Reloj-----")]
    [SerializeField]
    TextMeshProUGUI txtReloj;
    bool relojActivo;
    int horas;
    int minutos;

    // Start is called before the first frame update
    void Start()
    {
        relojActivo = true;
        StartCoroutine("RelojContador");
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    IEnumerator RelojContador()
    {
        while (relojActivo)
        {
            float minutosTotales = 0;
            RenderSettings.skybox.SetFloat("_Exposure", exposure);
            yield return new WaitForSeconds(velocidad);
            minutos++;
            minutosTotales++;
            if (minutos >= 60)
            {
                minutos = 0;
                horas++;
            }
            if (horas >= 24)
            {
                horas = 0;
                minutosTotales = 0;
            }
            txtReloj.text = (horas.ToString("D2")) + " : " + (minutos.ToString("D2"));
            if(horas < 12)
            {
                exposure += 1f / 720f;
            }
            else
            {
                exposure -= 1f / 720f;
            }

            print(exposure);

            RenderSettings.skybox.SetFloat("_Exposure", exposure);
            sol.transform.Rotate(Vector3.right * 0.25f);
        }   
    }
}
