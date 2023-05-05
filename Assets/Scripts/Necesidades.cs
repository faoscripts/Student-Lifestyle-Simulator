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

    public void AddNecesidad(float val, float mult = 0)
    {
        valor += val * multiplicadorVelocidad;

        valor = Mathf.Clamp(valor, 0, valorMaximo);
        multiplicadorVelocidad += mult;
    }
}

