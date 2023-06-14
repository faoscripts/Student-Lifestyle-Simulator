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
    public bool grab;

    public Necesidades[] statsSuma;
    public Necesidades[] statsRestar;

    [Header("Equipable")]
    public GameObject equipoPrefab;
    public float itemWeight;
    public bool consumible;
    public string soundName;
}

public enum EquipType
{
    Equipable,
    NoEquipable
}