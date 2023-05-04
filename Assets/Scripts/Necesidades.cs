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

    public void SetNecesidad(float i)
    {
        valor = i;
    }

    public void AddNecesidad(float i)
    {
        valor += i * multiplicadorVelocidad;
        if (!chute)
        {
            valor = Mathf.Clamp(valor, 0, Mathf.Infinity);
        }
        valor = Mathf.Clamp(valor, 0, valorMaximo);
    }
}
