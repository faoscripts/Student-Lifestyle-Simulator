using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("interactables")]
    public List<InteractableData> listIData;
    [Header("player")]
    public Necesidades[] playerListNecesidades;
    public float money;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    [Header("time")]
    public int day;
    public float time;
    public GameData(List<InteractableData> listIData, Necesidades[] playerListNecesidades,
        float money, Vector3 playerPosition, Quaternion playerRotation,int day, float time){
        this.listIData = listIData;
        this.playerListNecesidades = playerListNecesidades;
        this.money = money;
        this.playerPosition = playerPosition;
        this.playerRotation = playerRotation;
        this.day = day;
        this.time = time;
    }
}
