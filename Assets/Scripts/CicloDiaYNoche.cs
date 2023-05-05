using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CicloDiaYNoche : MonoBehaviour
{
    bool dia;
    [SerializeField]
    float velocidad;
    [SerializeField]
    GameObject sol;
    float exposure;

    [Header("-----Slider-----")]
    [SerializeField]
    Slider sliderSolLuna;
    [SerializeField]
    Image sliderHandle;
    [SerializeField]
    Sprite solSprite;
    [SerializeField]
    Sprite lunaSprite;

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
            
            //SKYBOX EXPOSURE
            if(horas < 12)
            {
                exposure += 1f / 720f;
            }
            else
            {
                exposure -= 1f / 720f;
            }
            RenderSettings.skybox.SetFloat("_Exposure", exposure);

            //SLIDER
            if (sliderSolLuna.value < 1)
            {
                sliderSolLuna.value += 1f / 1440;
            }
            else{
                sliderSolLuna.value = 0;
            }
            
            if(horas == 6)
            {
                sliderHandle.sprite = solSprite;
                dia = true;
            }
            if(horas == 20)
            {
                sliderHandle.sprite = lunaSprite;
                dia = false;
            }

            sol.transform.Rotate(Vector3.right * 0.25f);
        }   
    }
}
