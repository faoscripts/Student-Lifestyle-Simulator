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
    [SerializeField] int hourStartSun = 8;
    [SerializeField] int hoursDurationSun = 12;
    int sunHours = 0;
    bool boolSun = false;

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
        exposure = 0;
    }

    IEnumerator RelojContador()
    {
        while (relojActivo)
        {
            // RenderSettings.skybox.SetFloat("_Exposure", exposure);
            yield return new WaitForSeconds(velocidad);
            minutos++;
            if (minutos >= 60)
            {
                minutos = 0;
                horas++;
                if (boolSun) sunHours++;
            }
            if (horas >= 24)
            {
                StartCoroutine("NuevoDia");
            }

            txtReloj.text = (Mathf.FloorToInt(horas).ToString("D2")) + " : " + (minutos.ToString("D2"));
            
            //SKYBOX EXPOSURE

            if (horas >= hourStartSun && horas <= hourStartSun+hoursDurationSun)
            {
                boolSun = true;
                int halfDay = hoursDurationSun/2;
                if (sunHours <= halfDay) // morning
                {
                    // exposure = 1f / (4-4);
                }else{ // afternoon
                    exposure -= 1f / (hoursDurationSun*60/2);
                }
            }else{
                boolSun = false;
                sunHours = 0;
                exposure = 0;
            }
            // if(horas < 12)
            // {
            //     exposure += 1f / 720f;
            // }
            // else
            // {
            //     exposure -= 1f / 720f;
            // }
            // if (horas<8 || horas>20)
            // {
            //     exposure = 0;
            // }else{
            //     if (horas < 14)
            //     {
            //         exposure = (horas - 8) * 1 / 6;
            //     }else{
            //         exposure = 2-((horas - 8) * 1 / 6);
            //     }
            // }
            print("exposure = " + exposure);
            const float defaultBright = 0.1f;
            RenderSettings.skybox.SetFloat("_Exposure", exposure+defaultBright);
            DynamicGI.UpdateEnvironment();

            // SLIDER SUN AND MOON

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

            // const float adjustSyncSunHours = -90;

            // SUN ROTATION

            // float sunPosition = horas * 360 / 24;
            // sol.transform.rotation = Quaternion.Euler(sunPosition + adjustSyncSunHours, 0, 0);
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
