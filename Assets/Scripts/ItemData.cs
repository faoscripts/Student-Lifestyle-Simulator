using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Nuevo Item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    public string nombre;
    // public EquipType equipable;
    public bool grab;
    public Necesidades[] statsSuma;
    public Necesidades[] statsRestar;
    public string soundName;

    [Header("Equipable")]
    public GameObject equipoPrefab;
    public float itemWeight;
    public bool consumible;
    public bool rb;
    public bool spatialSound;
}

public enum EquipType
{
    Equipable,
    NoEquipable
}