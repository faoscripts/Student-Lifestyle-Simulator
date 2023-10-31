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
    public Necesidades[] statsSuma;
    public Necesidades[] statsRestar;
    public string soundName;

    [Header("Equipable")]
    public bool grab;
    public GameObject equipoPrefab;
    [Header("Complex Interaction")]
    public bool complex;
    public GameObject relatedGO;
    public GameObject resultGO;
    public bool grabed; // instantiate resultGO object in hand
    [Header("Gravity")]
    public bool rb;
    public float itemWeight;
    [Header("Other")]
    public bool consumible;
    public bool spatialSound;
    public AnimatorOverrideController AOC;
    public ParticleSystem particles;
}

public enum EquipType
{
    Equipable,
    NoEquipable
}