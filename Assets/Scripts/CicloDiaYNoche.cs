using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CicloDiaYNoche : MonoBehaviour
{
    public static int contadorDias;
    public static int daysTutorial = 1;
    // [SerializeField]
    // float velocidad;
    // [SerializeField]
    // GameObject sol;
    float exposure;
    // bool boolSun = false;

    [Header("-----SunConfig-----")]
    [SerializeField] private Light DirectionalLight;
    // [SerializeField] private LightSO Preset;
    private const float dayHours = 24;
    [SerializeField, Range(0,dayHours)] private float TimeOfDay;
    [SerializeField, Range(0,1)] private float speed;
    [SerializeField] int hourStartSun = 8;
    [SerializeField] int hoursDurationSun = 12;
    float sunHours = 0;

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
    // bool relojActivo;
    int horas;
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
        // relojActivo = true;
        // StartCoroutine("RelojContador");
        txtDia.gameObject.SetActive(false);
        exposure = 0;
    }

    void Update()
    {
        // TIME FLOW
        TimeOfDay += Time.deltaTime * speed;
        // TimeOfDay %= dayHours; // when arrive to dayHours reset to 0 | 8%24 = 8 | 24%24 = 0

        // NEXT DAY
        if (Mathf.FloorToInt(TimeOfDay) == dayHours)
        {
            NuevoDia();
        }

        // SUN ROTATION
        float timePercent = TimeOfDay/dayHours;
        DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 120f, 170f,0));

        // SLIDER SUN AND MOON
        sliderSolLuna.value = timePercent;
        
        if(TimeOfDay >= 7 && TimeOfDay <= 20)
        {
            sliderHandle.sprite = solSprite;
        }else{
            sliderHandle.sprite = lunaSprite;
        }

        // CLOCK
        horas = Mathf.FloorToInt(TimeOfDay);
        minutos = Mathf.FloorToInt(TimeOfDay % 1 * 60); // decimal to minutes || 0.5 = 30
        txtReloj.text = horas.ToString("D2") + " : " + minutos.ToString("D2"); // D2 string format convert 1 to 01, 2 to 02...

        //SKYBOX EXPOSURE || use TimeOfDay instead of hours because TOF is float and hours int
        if (TimeOfDay >= hourStartSun && TimeOfDay <= hourStartSun+hoursDurationSun) // here with the TOF the check ends earlier
        {
            sunHours = TimeOfDay - hourStartSun; // TOF makes the exposure go smoother with the decimals
            int halfSunHours = hoursDurationSun/2;

            if (sunHours <= halfSunHours) // morning
            {
                exposure = sunHours / halfSunHours;
            }else{ // afternoon
                exposure = (hoursDurationSun - sunHours) / halfSunHours;
            }
        }else{
            sunHours = 0;
            exposure = 0;
        }

        const float defaultBright = 0.1f;
        RenderSettings.skybox.SetFloat("_Exposure", exposure+defaultBright);
        DynamicGI.UpdateEnvironment();
    }

    IEnumerator BlackSceenFade(bool sleep = false)
    {
        const int SleepTime = 8;
        while (nuevoDia.color.a < 1)
        {
            nuevoDia.color = new Color(nuevoDia.color.r, nuevoDia.color.g, nuevoDia.color.b, nuevoDia.color.a + 0.1f);
            yield return new WaitForSeconds(0.02f);
        }

        if (sleep==true) TimeOfDay += SleepTime; // if sleep add sleep hours

        if (TimeOfDay > dayHours) // check if day change
        {
            contadorDias++; // update day counter
            txtDia.text = "Dia " + contadorDias; // update day text
            txtDia.gameObject.SetActive(true); // show day text
            TimeOfDay -= dayHours; // reset day hours
        }

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
        StartCoroutine(nameof(BlackSceenFade),false);
    }

    public void Sleep()
    {
        StartCoroutine(nameof(BlackSceenFade),true);
    }
}
