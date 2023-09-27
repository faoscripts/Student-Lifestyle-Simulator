using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Necesidades
{
    public string nombre;
    public float valor;
    public float valorMaximo = 100;
    public float multiplicadorVelocidad;
    public bool necesidadVital;

    public void SetNecesidad(float val, float mult)
    {
        Debug.Log("val = " + val);
        valor += val;
        valor = Mathf.Clamp(valor, 0, valorMaximo);
        multiplicadorVelocidad += mult;
    }

    public void AddNecesidad(float val)
    {
        valor += val * multiplicadorVelocidad;
        valor = Mathf.Clamp(valor, 0, valorMaximo);
    }
}

