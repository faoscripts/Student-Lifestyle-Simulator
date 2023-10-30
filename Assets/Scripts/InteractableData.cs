using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class InteractableData
{
    public string nameInteractable;
    public Vector3 position;
    public Quaternion rotation;
    public InteractableData(string nameInteractable, Vector3 position, Quaternion rotation){
        this.nameInteractable = nameInteractable;
        this.position = position;
        this.rotation = rotation;
    }
}
