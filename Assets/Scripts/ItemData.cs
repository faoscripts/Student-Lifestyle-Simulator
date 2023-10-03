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
    public AnimatorOverrideController AOC;
    public ParticleSystem particles;
    public bool complex;
    public GameObject relatedGO;
    public GameObject resultGO;
    public bool consumible;
    public bool rb;
    public float itemWeight;
    public bool spatialSound;
}

public enum EquipType
{
    Equipable,
    NoEquipable
}