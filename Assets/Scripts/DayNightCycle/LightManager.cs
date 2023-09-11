using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    // [SerializeField] private LightSO Preset;
    private const float dayHours = 24;
    [SerializeField, Range(0,dayHours)] private float TimeOfDay;
    [SerializeField, Range(0,1)] private float speed;

    // private void Start()
    // {
    //     StartCoroutine(nameof(RelojContador));
    // }

    // IEnumerator RelojContador()
    // {
    //     while (true)
    //     {  
    //         if(Application.isPlaying){
    //             yield return new WaitForSeconds(speed); // speed
    //             // TimeOfDay += Time.deltaTime
    //             TimeOfDay += 0.01f; // tics light does | high value light jums | low value smooth use more resources
    //             TimeOfDay %= dayHours; // when arrive reset to 0 | 8%24 = 8 | 24%24 = 0
    //         }
    //         UpdateLighting(TimeOfDay/dayHours);
    //     }
    // }

    private void Update()
    { 
        if(Application.isPlaying){
            TimeOfDay += Time.deltaTime * speed;
            TimeOfDay %= dayHours; // when arrive reset to 0 | 8%24 = 8 | 24%24 = 0
        }
        UpdateLighting(TimeOfDay/dayHours);
    }

    private void UpdateLighting(float timePercent)
    {
        // RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        // RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        
        // DirectionalLight.color = Preset.AmbientColor.Evaluate(timePercent);
        DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 120f, 170f,0)); // -120 || flicks
        // DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0)); // smooth
    }

    // private void OnValidate()
    // {
    //     if(DirectionalLight != null) return;
    // }
}
