using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDiaYNoche : MonoBehaviour
{
    bool dia;
    [SerializeField]
    float velocidad;
    [SerializeField]
    GameObject sol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sol.transform.Rotate(Vector3.right * velocidad * Time.deltaTime);
    }
}
