using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Nuevo Item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    public string nombre;
    public EquipType equipable;
    public GrabType tipoCoger;

    public Necesidades[] statsSuma;
    public Necesidades[] statsRestar;

    [Header("Equipable")]
    public GameObject equipoPrefab;
}

public enum EquipType
{
    Equipable,
    NoEquipable
}

public enum GrabType
{
    
}