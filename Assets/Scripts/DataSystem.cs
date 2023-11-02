using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataSystem : MonoBehaviour
{
    public static bool newgame = true;
    public GameObject[] interactableGrabList;
    public static void SaveGame(){
        print("save game");
        ItemController[] interactables = FindObjectsOfType<ItemController>();
        List<InteractableData> interactablesGrab = new();
        foreach (ItemController interactable in interactables)
        {
            InteractableData idata = new InteractableData(interactable.item.nombre, interactable.gameObject.transform.position, interactable.gameObject.transform.rotation);
            if (interactable.item.grab) interactablesGrab.Add(idata);
        }
        SaveToJson(interactablesGrab);
    }

    public static void SaveToJson(List<InteractableData> gameData){
        // PlayerData data = new PlayerData(player);
        GameObject playerBody = FindObjectOfType<NecesidadController>().gameObject;
        CicloDiaYNoche CDN = FindObjectOfType<CicloDiaYNoche>();
        GameData data = new GameData(gameData,playerBody.GetComponent<NecesidadController>().necesidades,
            playerBody.GetComponent<PlayerMovement>().money, playerBody.transform.position,playerBody.transform.rotation,
            CicloDiaYNoche.contadorDias,CDN.TimeOfDay);
        
        string json = JsonUtility.ToJson(data,true);
        File.WriteAllText(Application.dataPath + "/GameDataFile.json", json);
    }

    public static GameData LoadFromJson(){
        string json = File.ReadAllText(Application.dataPath + "/GameDataFile.json");
        GameData data = JsonUtility.FromJson<GameData>(json);
        return data;
    }

    public void LoadGame(){
        StartCoroutine(nameof(LoadScreen));
        CleanMap();
        GameData data = LoadFromJson();

        GameObject playerBody = FindObjectOfType<NecesidadController>().gameObject;
        playerBody.GetComponent<NecesidadController>().necesidades=data.playerListNecesidades;
        playerBody.GetComponent<PlayerMovement>().money = data.money;
        playerBody.transform.position = data.playerPosition;
        playerBody.transform.rotation = data.playerRotation;

        CicloDiaYNoche CDN = FindObjectOfType<CicloDiaYNoche>();
        CicloDiaYNoche.contadorDias = data.day;
        CDN.TimeOfDay = data.time;

        foreach (InteractableData interactable in data.listIData)
        {
            string nameInteractable = interactable.nameInteractable.Split(" ")[0];
            GameObject interactablePrefab = Array.Find(interactableGrabList,
                interactableList => interactableList.name == nameInteractable);
            Instantiate(interactablePrefab, interactable.position, interactable.rotation);
        }
    }

    public static void CleanMap(){
        ItemController[] interactables = FindObjectsOfType<ItemController>();
        foreach (ItemController interactable in interactables)
        {
            if (interactable.item.grab) Destroy(interactable.gameObject);
        }
    }

    IEnumerator LoadScreen(){
        OptionsMenu omenu = FindObjectOfType<OptionsMenu>();
        if (omenu.pauseScreen.activeInHierarchy) omenu.PauseScreen();
        CicloDiaYNoche CDN = FindObjectOfType<CicloDiaYNoche>();
        Image nuevoDia = CDN.nuevoDia;
        while (nuevoDia.color.a < 1)
        {
            nuevoDia.color = new Color(nuevoDia.color.r, nuevoDia.color.g, nuevoDia.color.b, nuevoDia.color.a + 0.1f);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(2);

        while (nuevoDia.color.a > 0)
        {
            nuevoDia.color = new Color(nuevoDia.color.r, nuevoDia.color.g, nuevoDia.color.b, nuevoDia.color.a - 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
