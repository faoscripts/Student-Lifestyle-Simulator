using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CicloDiaYNoche : MonoBehaviour
{
    float contadorDias;
    
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
    float horas;
    int minutos;

    [Header("-----Nuevo Dia-----")]
    [SerializeField]
    Image nuevoDia;
    [SerializeField]
    TextMeshProUGUI txtDia;

    // Start is called before the first frame update
    void Start()
    {
        contadorDias = 1;
        horas = 8;
        relojActivo = true;
        StartCoroutine("RelojContador");
        txtDia.gameObject.SetActive(false);
    }

    IEnumerator RelojContador()
    {
        while (relojActivo)
        {
            RenderSettings.skybox.SetFloat("_Exposure", exposure);
            yield return new WaitForSeconds(velocidad);
            minutos++;
            if (minutos >= 60)
            {
                minutos = 0;
                horas++;
            }
            if (horas >= 24)
            {
                StartCoroutine("NuevoDia");
            }

            txtReloj.text = (Mathf.FloorToInt(horas).ToString("D2")) + " : " + (minutos.ToString("D2"));
            
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
                // sliderSolLuna.value += 1f / 1440;
                sliderSolLuna.value = horas * 1 / 24;
            }
            else{
                sliderSolLuna.value = 0;
            }
            
            if(horas >= 6 && horas <= 20)
            {
                sliderHandle.sprite = solSprite;
                dia = true;
            }
            if(horas >= 20 || horas >= 0 && horas <= 6 )
            {
                sliderHandle.sprite = lunaSprite;
                dia = false;
            }

            const float adjustSyncSunHours = -90;

            float sunPosition = horas * 360 / 24;
            sol.transform.rotation = Quaternion.Euler(sunPosition + adjustSyncSunHours, 0, 0);
        }   
    }

    IEnumerator NuevoDiaCoroutine()
    {
        horas = 0;
        contadorDias++;
        txtDia.text = "Dia " + contadorDias;
        while (nuevoDia.color.a < 1)
        {
            nuevoDia.color = new Color(nuevoDia.color.r, nuevoDia.color.g, nuevoDia.color.b, nuevoDia.color.a + 0.1f);
            yield return new WaitForSeconds(0.02f);
        }

        txtDia.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        txtDia.gameObject.SetActive(false);

        while (nuevoDia.color.a > 0)
        {
            nuevoDia.color = new Color(nuevoDia.color.r, nuevoDia.color.g, nuevoDia.color.b, nuevoDia.color.a - 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator SleepCoroutine()
    {
        horas += 8;
        // contadorDias++;
        // txtDia.text = "D�a " + contadorDias;
        while (nuevoDia.color.a < 1)
        {
            nuevoDia.color = new Color(nuevoDia.color.r, nuevoDia.color.g, nuevoDia.color.b, nuevoDia.color.a + 0.1f);
            yield return new WaitForSeconds(0.02f);
        }

        txtDia.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        txtDia.gameObject.SetActive(false);

        while (nuevoDia.color.a > 0)
        {
            nuevoDia.color = new Color(nuevoDia.color.r, nuevoDia.color.g, nuevoDia.color.b, nuevoDia.color.a - 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void NuevoDia()
    {
        StartCoroutine("NuevoDiaCoroutine");
    }

    public void Sleep()
    {
        StartCoroutine("SleepCoroutine");
    }
}
