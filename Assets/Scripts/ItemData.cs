using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactuable", menuName = "Nuevo Interactuable")]
public class InteractuableData : ScriptableObject
{
    [Header("info")]
    public string nombre;
    public GrabType tipoCoger;

    public Necesidades[] statsSuma;
    public Necesidades[] statsRestar;

    [Header("Equipable")]
    public GameObject equipoPrefab;
}
public enum GrabType
{
    
}